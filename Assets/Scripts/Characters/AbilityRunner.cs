using System;
using UnityEngine;
public class AbilityRunner : MonoBehaviour {
    // Input-based ability events
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnAimAtEnemy;
    public event Action<Vector2> OnAimAtPlayer;
    public event Action OnGuard;
    public event Action OnRest;

    // Collision-based ability events
    public event Action<Character> OnEnemyCollision;
    public event Action<Character> OnPlayerCollision;

    // Health-based ability events
    public event Action<int, int> OnDamageTaken;

    [field: SerializeField] public Vector2 AimVector { get; private set; }
    [field: SerializeField] public float MaxAimMagnitude { get; private set; } = 5f;

    public void SetAimVector(Vector2 vector) {
        Vector2 clampedVector = Vector2.ClampMagnitude(vector, MaxAimMagnitude);

        AimVector = clampedVector;
    }
    public void ExecuteAbilitiesOnMoveEvent() {
        OnMove?.Invoke(AimVector);
    }
    public void ExecuteAbilitiesOnAimAtEnemyEvent() {
        OnAimAtEnemy?.Invoke(AimVector);
    }
    public void ExecuteAbilitiesOnAimAtPlayerEvent() {
        OnAimAtPlayer?.Invoke(AimVector);
    }
    public void ExecuteAbilitiesOnGuardEvent() {
        OnGuard?.Invoke();
    }
    public void ExecuteAbilitiesOnRestEvent() {
        OnRest?.Invoke();
    }
    public void ExecuteAbilitiesOnEnemyCollision(Character enemyCharacterCollided) {
        OnEnemyCollision?.Invoke(enemyCharacterCollided);

        Debug.Log(gameObject.name + " attacked " + enemyCharacterCollided.gameObject.name);
    }
    public void ExecuteAbilitiesOnPlayerCollision(Character playerCharacterCollided) {
        OnPlayerCollision?.Invoke(playerCharacterCollided);

        Debug.Log(gameObject.name + " helped " + playerCharacterCollided.gameObject.name);
    }      
    public void ExecuteAbilitiesOnDamageTaken(int currentHealth, int damageTaken) {
        OnDamageTaken?.Invoke(currentHealth, damageTaken);

        Debug.Log("damage");
    }    
}