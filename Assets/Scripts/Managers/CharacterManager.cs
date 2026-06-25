using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public static CharacterManager Instance;

    public event Action OnCharacterRegistryChange;
    public event Action OnSelectCharacterOnClickStart;
    public event Action OnSelectCharacterOnClickEnd;

    [field: SerializeField] public Character SelectedCharacter { get; private set; }

    public List<Character> playerCharacters = new List<Character>();
    public List<Character> enemyCharacters = new List<Character>();

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    public void UpdateSelectedCharacter(Character selectedCharacter) {
        Instance.SelectedCharacter = selectedCharacter;

        OnSelectCharacterOnClickStart?.Invoke();
    }

    public void RegisterCharacter(Character character, CharacterType characterType) {
        if (playerCharacters.Contains(character) || enemyCharacters.Contains(character)) {
            UnregisterCharacter(character);
        }

        switch (characterType) {
            case CharacterType.Player:
                playerCharacters.Add(character);
                break;
            case CharacterType.Enemy:
                enemyCharacters.Add(character);
                break;
        }

        OnCharacterRegistryChange?.Invoke();
    }

    public void UnregisterCharacter(Character character) {
        if (playerCharacters.Contains(character)) {
            playerCharacters.Remove(character);
        }
        else if (enemyCharacters.Contains(character)) {
            enemyCharacters.Remove(character);
        }

        OnCharacterRegistryChange?.Invoke();
    }
}