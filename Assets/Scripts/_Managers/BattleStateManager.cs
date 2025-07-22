using UnityEngine;

public class BattleStateManager : MonoBehaviour {
    public static BattleStateManager Instance { get; private set; }
    public IAbility ActiveAbility { get; private set; }

    public BattleState _currentGameState;

    public void SetActiveAbility(IAbility ability) {
        ActiveAbility = ability;
    }

    void Start() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        _currentGameState = BattleState.Cutscene;
    }

    public void ChangeBattleState(BattleState state) {
        if (_currentGameState == state) { return; }

        _currentGameState = state;

        switch (state) {
            case BattleState.Cutscene:
                RunCutscene();
                break;
            case BattleState.WaitingForPlayerInput:
                RunWaitingForPlayerInput();
                break;
            case BattleState.PlayerTurn:
                RunPlayerTurn();
                break;
            case BattleState.EnemyTurn:
                RunEnemyTurn();
                break;
            case BattleState.PlayerWin:
                RunPlayerWin();
                break;
            case BattleState.EnemyWin:
                RunEnemyWin();
                break;
        }
    }
    public void RunCutscene() {

    }
    public void RunWaitingForPlayerInput() {
        foreach (Character character in CharacterManager.Instance._playerCharacters) {
            character.GetComponent<CharacterUI>().DisableAbilitiesUI();
        }
    }
    public void RunPlayerTurn() {
    }
    public void RunEnemyTurn() {

    }
    public void RunPlayerWin() {

    }
    public void RunEnemyWin() { 
    
    }
}