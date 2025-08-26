using TMPro;
using UnityEngine;

public class CharacterSelectedUI : MonoBehaviour {
    public static Color PlayerSelectedColor;
    public static Color EnemySelectedColor;

    private Character _character;
    private SpriteRenderer _characterSelectedHighlightUI;
    private TextMeshProUGUI _nameUI;

    public void DisplayCharacterSelectedUI() {
        _characterSelectedHighlightUI.gameObject.SetActive(true);
        _nameUI.gameObject.SetActive(true);
    }
    public void HideCharacterSelectedUI() {
        _characterSelectedHighlightUI.gameObject.SetActive(false);
        _nameUI.gameObject.SetActive(false);
    }

    private void Awake() {
        if (PlayerSelectedColor == default) {
            PlayerSelectedColor = HexToColor("#5FFFA5");
            EnemySelectedColor = HexToColor("#FF2F53");
        }

        _character = GetComponentInParent<Character>();
        _characterSelectedHighlightUI = GetComponentInChildren<SpriteRenderer>(true); // true = include inactive children in search
        _nameUI = GetComponentInChildren<TextMeshProUGUI>(true); // true = include inactive children in search
    }
    private void Start() {
        AssignCharacterSelectedColor();

        _nameUI.text = _character.Name;
    }

    private void AssignCharacterSelectedColor() {
        CharacterType characterType = _character.CharacterType;
        if (characterType == CharacterType.Player) {
            _characterSelectedHighlightUI.color = PlayerSelectedColor;
        }
        else if (characterType == CharacterType.Enemy) {
            _characterSelectedHighlightUI.color = EnemySelectedColor;
        }  
    }

    public static Color HexToColor(string hex) {
        if (ColorUtility.TryParseHtmlString(hex, out var color)) {
            return color;
        }
        Debug.LogWarning($"Invalid hex code: {hex}. Defaulting to white.");
        return Color.white;
    }
}