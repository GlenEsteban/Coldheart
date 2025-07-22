using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
    private CharacterSelectedUI _characterSelectedUI;
    private AbilitiesUI _abilitiesUI;
    private AbilityPromptsUI _abililtyPromptsUI;

    private Character _character;
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
        CharacterManager.Instance.OnSelectCharacter += CharacterSelectedCheck;
    }
    private void OnDisable() {
        CharacterManager.Instance.OnSelectCharacter -= CharacterSelectedCheck;
    }
    private void Awake() {
        _character = GetComponent<Character>();

        _characterSelectedUI = GetComponentInChildren<CharacterSelectedUI>();
        _abilitiesUI = GetComponentInChildren<AbilitiesUI>();
        _abililtyPromptsUI = GetComponentInChildren<AbilityPromptsUI>();
    }
    private void Start() {
        CharacterManager.Instance.OnSelectCharacter += CharacterSelectedCheck; 
        _hasInitialized = true;
    }

    private void CharacterSelectedCheck() {
        if (CharacterManager.Instance.SelectedCharacter != null &&
            CharacterManager.Instance.SelectedCharacter.gameObject == _character.gameObject) {
            _characterSelectedUI.DisplayCharacterSelectedUI();
            _abilitiesUI.DisplayAbliltiesUI();
        }
        else {
            _characterSelectedUI.HideCharacterSelectedUI();
            _abilitiesUI.HideAbilitiesUI();
        }
    }
}