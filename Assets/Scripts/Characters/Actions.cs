using System;
using UnityEngine;

public class Actions : MonoBehaviour {
    public event Action<Vector2> OnMove;
    public event Action OnGuard;
    public event Action OnRest;

    [field: SerializeField] public bool IsAiming { get; private set; }
    [field: SerializeField] public Vector2 AimVector { get; private set; }
    [field: SerializeField] public float MaxAimMagnitude { get; private set; } = 5f;

    public void SetIsAiming(bool state) {
        IsAiming = state;
    }
    public void SetAimVector(Vector2 vector) {
        Vector2 clampedVector = Vector2.ClampMagnitude(vector, MaxAimMagnitude);

        AimVector = clampedVector;
    }
    public void Move() {
        OnMove?.Invoke(AimVector);
    }    
    public void Guard() {
        OnGuard?.Invoke();
    }    
    public void Rest() {
        OnRest?.Invoke();
    }
}
