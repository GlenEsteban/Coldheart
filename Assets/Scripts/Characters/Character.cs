using UnityEngine;

public class Character : MonoBehaviour {
    [field: SerializeField, ReadOnly] public string CharacterName { get; private set; }
    [field: SerializeField, ReadOnly] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Actions Actions { get; private set; }
    [field: SerializeField] public Movement Movement { get; private set; }
    [field: SerializeField] public ColliderInteractions ColliderInteractions { get; private set; }

    private void SetCharacterType() {
        switch (LayerMask.LayerToName(gameObject.layer)) {
            case "Player":
                CharacterType = CharacterType.Player;
                break;

            case "Enemy":
                CharacterType = CharacterType.Enemy;
                break;

            default:
                CharacterType = CharacterType.NPC;
                break;
        }
    }
    private void Awake() {
        CharacterName = gameObject.name;

        SetCharacterType();

        Health = GetComponent<Health>();
        Actions = GetComponent<Actions>();
        Movement = GetComponent<Movement>();
    }
    private void OnEnable() {
        CharacterManager.Instance.RegisterCharacter(this, CharacterType);
    }
    private void OnDisable() {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}