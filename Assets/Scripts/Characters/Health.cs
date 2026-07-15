using UnityEngine;
using System;

public class Health : MonoBehaviour {
    public event Action OnHealthChanged;

    [field: SerializeField] public int CurrentHealth { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;

    [SerializeField] private AbilityRunner abilityRunner;

    private void Start() {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damageAmount) {
        CurrentHealth -= damageAmount;

        Mathf.Max(CurrentHealth, 0);

        abilityRunner.ExecuteAbilitiesOnDamageTaken(CurrentHealth, damageAmount);

        OnHealthChanged?.Invoke();
    }
}
