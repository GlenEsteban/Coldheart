using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    public event Action Transition;
    public event Action PlayerTurn;
    public event Action EnemyTurn;
    public event Action GameWin;
    public event Action GameOver;

    public GameState currentGameState;

    private void Awake() {
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
        Debug.Log("EVENT: Transition");

        Transition?.Invoke();
    }
    public void RunPlayerTurn() {
        Debug.Log("EVENT: Player Turn");

        PlayerTurn?.Invoke();
    }
    public void RunEnemyTurn() {
        Debug.Log("EVENT: Enemy Turn");

        EnemyTurn?.Invoke();
    }
    public void RunGameWin() {
        Debug.Log("EVENT: Game Win");

        GameWin?.Invoke();
    }
    public void RunGameOver() {    
        Debug.Log("EVENT: Game Over");

        GameOver?.Invoke();
    }
}