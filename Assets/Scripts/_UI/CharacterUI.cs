using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
    private CharacterSelectedUI _characterSelectedUI;
    private AbilitiesUI _abilitiesUI;
    private AbilityPromptsUI _abililtyPromptsUI;

    private bool _hasInitialized = false;

    public void EnableAbilitiesUI() {
        _abilitiesUI.DisplayAbliltiesUI();
    }
    public void DisableAbilitiesUI() {
        _abilitiesUI.HideAbilitiesUI();
    }
    public void EnableCharacterSelectedUI() {
        _characterSelectedUI.DisplayCharacterSelectedUI();
    }
    public void DisableCharacterSelectedUI() {
        _characterSelectedUI.HideCharacterSelectedUI();
    }
    public void AddAbilityButtonUI(Button abilityButtonUI) {
        _abilitiesUI.AddAbliltyButtonUI(abilityButtonUI);
    }
    public void AddAbilityPromptUI(GameObject abilitypromptUI) {
        _abililtyPromptsUI.AddAbilityPromptUI(abilitypromptUI);
    }

    private void OnEnable() {
        if (!_hasInitialized) { return; } // Lazy initialization pattern ensures resubscription when game object is reenabled
        PlayerInputManager.Instance.OnClickStart += CheckIfCharacterSelectedOnClickStart;
        PlayerInputManager.Instance.OnClickEnd += CheckIfCharacterSelectedOnFocusedClick;
    }
    private void OnDisable() {
        PlayerInputManager.Instance.OnClickStart -= CheckIfCharacterSelectedOnClickStart;
        PlayerInputManager.Instance.OnClickEnd -= CheckIfCharacterSelectedOnFocusedClick;
    }
    private void Awake() {
        _characterSelectedUI = GetComponentInChildren<CharacterSelectedUI>();
        _abilitiesUI = GetComponentInChildren<AbilitiesUI>();
        _abililtyPromptsUI = GetComponentInChildren<AbilityPromptsUI>();
    }
    private void Start() {
        PlayerInputManager.Instance.OnClickStart += CheckIfCharacterSelectedOnClickStart; 
        PlayerInputManager.Instance.OnClickEnd += CheckIfCharacterSelectedOnFocusedClick;
        _hasInitialized = true;
    }
    
    private void CheckIfCharacterSelectedOnClickStart() {
        if (PlayerInputManager.Instance.SelectedCharacterOnClickStart == gameObject.GetComponent<Character>()) {
            _characterSelectedUI.DisplayCharacterSelectedUI();
        }
        else {
            _characterSelectedUI.HideCharacterSelectedUI();
        }
    }

    private void CheckIfCharacterSelectedOnFocusedClick() {
        if (PlayerInputManager.Instance.SelectedCharacterOnClickEnd == gameObject.GetComponent<Character>()) {
            _abilitiesUI.DisplayAbliltiesUI();
        }
        else {
            _abilitiesUI.HideAbilitiesUI();
        }
    }
}