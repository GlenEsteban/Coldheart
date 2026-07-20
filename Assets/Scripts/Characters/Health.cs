using UnityEngine;
using System;
using System.Collections;

public class Health : MonoBehaviour {
    public event Action<int, int> OnDamageTaken;
    public event Action OnDeath;

    [field: SerializeField] public int CurrentHealth { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;

    private void Start() {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damageAmount) {
        CurrentHealth -= damageAmount;

        Mathf.Max(CurrentHealth, 0);

        if (CurrentHealth > 0) {
            OnDamageTaken?.Invoke(CurrentHealth, damageAmount);
        }
        else {
            OnDeath?.Invoke();
        }

        OnDamageTaken?.Invoke(CurrentHealth, MaxHealth);
    }
}
