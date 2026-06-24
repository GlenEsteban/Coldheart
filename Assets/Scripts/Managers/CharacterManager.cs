using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public static CharacterManager Instance;

    public event Action OnCharacterRegistryChange;
    public event Action OnSelectCharacterOnClickStart;
    public event Action OnSelectCharacterOnClickEnd;

    [field: SerializeField] public Character SelectedCharacter { get; set; }

    public List<Character> _playerCharacters = new List<Character>();
    public List<Character> _enemyCharacters = new List<Character>();

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
        if (_playerCharacters.Contains(character) || _enemyCharacters.Contains(character)) {
            UnregisterCharacter(character);
        }

        switch (characterType) {
            case CharacterType.Player:
                _playerCharacters.Add(character);
                break;
            case CharacterType.Enemy:
                _enemyCharacters.Add(character);
                break;
        }

        OnCharacterRegistryChange?.Invoke();
    }

    public void UnregisterCharacter(Character character) {
        if (_playerCharacters.Contains(character)) {
            _playerCharacters.Remove(character);
        }
        else if (_enemyCharacters.Contains(character)) {
            _enemyCharacters.Remove(character);
        }

        OnCharacterRegistryChange?.Invoke();
    }
}