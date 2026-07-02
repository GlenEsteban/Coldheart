using System;
using UnityEngine;

public abstract class ActiveAbility : MonoBehaviour {
    private ActiveAbilityRunner abilityRunner;

    private void Awake() {
        abilityRunner = GetComponent<ActiveAbilityRunner>();
    }
    private void OnEnable() {
        if (abilityRunner != null) {
            abilityRunner.RegisterActiveAbility(this);
        }
    }
    private void OnDisable() {
        if (abilityRunner != null) {
            abilityRunner.UnregisterActiveAbility(this);
        }
    }
    public abstract void Execute(Vector2 aimVector);
}