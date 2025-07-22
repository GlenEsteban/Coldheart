using System;
using UnityEngine;

public class Character : MonoBehaviour {
    [field: SerializeField] public string Name{ get; private set; }
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public IAbility[] Abilities { get; private set; }

    private void Start() {
        CharacterManager.Instance.RegisterCharacter(this, CharacterType);
    }
    private void OnDisable() {
        CharacterManager.Instance.UnregisterCharacter(this);
    }
}
