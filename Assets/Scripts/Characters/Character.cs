using UnityEngine;

public class Character : MonoBehaviour {
    [field: SerializeField] public string Name{ get; private set; }
    [field: SerializeField] public CharacterType CharacterType { get; private set; }

    private void Start() {
        Name = gameObject.name;

        CharacterManager.Instance.RegisterCharacter(this, CharacterType);
    }
    private void OnDisable() {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}