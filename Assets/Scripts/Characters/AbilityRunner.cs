using System;
using System.Collections.Generic;
using UnityEngine;
public class AbilityRunner : MonoBehaviour {
    // Input-based ability events
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnAimAtEnemy;
    public event Action<Vector2> OnAimAtPlayer;
    public event Action OnGuard;
    public event Action OnRest;

    // Collision-based ability events
    public event Action OnEnemyCollision;
    public event Action OnPlayerCollision;

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
}