using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    public GameState currentGameState;

    private void Start() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        currentGameState = GameState.Transition;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeGameState(GameState.Transition);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeGameState(GameState.PlayerTurn);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeGameState(GameState.EnemyTurn);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeGameState(GameState.GameWin);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            ChangeGameState(GameState.GameOver);
        }
    }
    public void ChangeGameState(GameState state) {
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