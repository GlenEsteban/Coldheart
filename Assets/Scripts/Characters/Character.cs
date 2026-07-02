using UnityEngine;

public class Character : MonoBehaviour {
    [field: SerializeField, ReadOnly] public string characterName { get; private set; }
    [field: SerializeField, ReadOnly] public CharacterType characterType { get; private set; }

    private void SetCharacterType() {
        switch (LayerMask.LayerToName(gameObject.layer)) {
            case "Player":
                characterType = CharacterType.Player;
                break;

            case "Enemy":
                characterType = CharacterType.Enemy;
                break;

            default:
                characterType = CharacterType.NPC;
                break;
        }
    }
    private void Start() {
        characterName = gameObject.name;

        SetCharacterType();

        CharacterManager.Instance.RegisterCharacter(this, characterType);
    }
    private void OnDisable() {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}