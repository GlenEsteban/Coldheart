using UnityEngine;

public class Character : MonoBehaviour {
    [field: SerializeField, ReadOnly] public string CharacterName { get; private set; }
    [field: SerializeField, ReadOnly] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public ActiveAbilityRunner ActiveAbilityRunner { get; private set; }
    [field: SerializeField] public Collider2D HitCollider { get; private set; }

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

        ActiveAbilityRunner = GetComponent<ActiveAbilityRunner>();
    }
    private void OnEnable() {
        CharacterManager.Instance.RegisterCharacter(this, CharacterType);
    }
    private void OnDisable() {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}