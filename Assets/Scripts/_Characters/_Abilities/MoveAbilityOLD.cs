using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveAbilityOLD : MonoBehaviour, IAbility {
    public AbilityType AbilityType { get; private set; }

    [SerializeField, Range(1, 10)] private float _movementSpeed = 2f;
    [SerializeField] string _abilityButtonUIPrefabFilePath = "Prefabs/Ability_Move_ButtonUI";
    [SerializeField] string _abilityExecutionPromptUIPrefabFilePath = "Prefabs/Ability_Move_PromptUI";

    CharacterUI _characterUI;
    Button _abilityButtonUIPrefab;
    GameObject _abilityExecutionPromptUIPrefab;
    Button _abilityButtonUIInstance;
    GameObject _abilityExecutionPromptUIInstance;

    private bool _hasInitialized = false;
    private bool _isPromptingForInput = false;
    private bool _isExecutingAbility = false;

    private Vector2 _targetPosition;
    private float _distanceFromTarget = 0;


    public void DisplayAbilityExecutionPrompt() {
        _isPromptingForInput = true;
        _abilityExecutionPromptUIInstance.gameObject.SetActive(true);

        _characterUI.DisableAbilitiesUI();
        _characterUI.DisableCharacterSelectedUI();

        BattleStateManager.Instance.ChangeBattleState(BattleState.WaitingForPlayerInput);
    }

    private void OnEnable() {
        if (!_hasInitialized) { return; }
        PlayerInputManager.Instance.OnColliderClickStart += CheckForAbilityInput;
        _abilityButtonUIInstance.onClick.AddListener(DisplayAbilityExecutionPrompt);
    }

    private void OnDisable() {
        PlayerInputManager.Instance.OnColliderClickStart -= CheckForAbilityInput;
        _abilityButtonUIInstance.onClick.RemoveListener(DisplayAbilityExecutionPrompt);
    }
    private void Awake() {
        _characterUI = GetComponent<CharacterUI>();

        CacheAbilityAssetsFromResources();
    }
    private void Start() {        
        PlayerInputManager.Instance.OnColliderClickStart += CheckForAbilityInput;
        _abilityButtonUIInstance.onClick.AddListener(DisplayAbilityExecutionPrompt);

        _hasInitialized = true;

        _characterUI.AddAbilityButtonUI(_abilityButtonUIInstance);
        _characterUI.AddAbilityPromptUI(_abilityExecutionPromptUIInstance);

        _abilityExecutionPromptUIInstance.gameObject.SetActive(false);
    }
    private void Update() {
        if (_isExecutingAbility) {
            _distanceFromTarget = ((Vector3)_targetPosition - transform.position).magnitude;
            if (_distanceFromTarget > 0.05) {
                transform.position = Vector3.Lerp(transform.position, (Vector3) _targetPosition, Time.deltaTime * _movementSpeed);
            }
            else {
                _isExecutingAbility = false;
            }
        }
    }

    private void CacheAbilityAssetsFromResources() {
        _abilityButtonUIPrefab = Resources.Load<Button>(_abilityButtonUIPrefabFilePath);
        _abilityExecutionPromptUIPrefab = Resources.Load<GameObject>(_abilityExecutionPromptUIPrefabFilePath);

        _abilityButtonUIInstance = Instantiate(_abilityButtonUIPrefab);
        _abilityExecutionPromptUIInstance = Instantiate(_abilityExecutionPromptUIPrefab);
    }
    private void CheckForAbilityInput() {
        if (!_isPromptingForInput) { return; }

        Collider2D targetCollider = _abilityExecutionPromptUIInstance.GetComponent<Collider2D>();
        if (!PlayerInputManager.Instance.HasClickedOnTargetCollider(targetCollider)) { return; }

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        _targetPosition = (Vector2) worldPos; // Typecast Vector2 in order to set z position to 0;

        HideAbilityExecutionPrompt();

        Execute(this.gameObject);
    }

    private void HideAbilityExecutionPrompt() {
        _isPromptingForInput = false;
        _abilityExecutionPromptUIInstance.gameObject.SetActive(false);
    }

    public void Execute(GameObject user) {
        _isExecutingAbility = true;
    }
}