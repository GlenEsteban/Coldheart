using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    public GameState currentGameState;

    void Start() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        currentGameState = GameState.Transition;
    }

    public void ChangeBattleState(GameState state) {
        if (currentGameState == state) { return; }

        currentGameState = state;

        switch (state) {
            case GameState.Transition:
                RunPlayerTurn();
                break;
            case GameState.PlayerTurn:
                RunPlayerTurn();
                break;
            case GameState.EnemyTurn:
                RunEnemyTurn();
                break;
            case GameState.GameWin:
                RunGameWin();
                break;
            case GameState.GameOver:
                RunGameOver();
                break;
        }
    }

    public void RunTransition() {
    }

    public void RunPlayerTurn() {
    }

    public void RunEnemyTurn() {
    }

    public void RunGameWin() {
    }

    public void RunGameOver() {     
    }
}