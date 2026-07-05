using System;
using UnityEngine;

public abstract class ActiveAbility : MonoBehaviour {
    private ActiveAbilityRunner abilityRunner;

    protected void Awake() {
        abilityRunner = GetComponent<ActiveAbilityRunner>();
    }
    protected void OnEnable() {
        if (abilityRunner != null) {
            abilityRunner.RegisterActiveAbility(this);
        }
    }
    protected void OnDisable() {
        if (abilityRunner != null) {
            abilityRunner.UnregisterActiveAbility(this);
        }
    }
    public abstract void Execute(Vector2 aimVector);
}