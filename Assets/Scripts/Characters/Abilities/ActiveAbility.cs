using UnityEngine;

[RequireComponent(typeof(ActiveAbilityRunner))]
public abstract class ActiveAbility : MonoBehaviour {
    [field: SerializeField] public float MaxAimMagnitude { get; private set; } = 5f;

    private ActiveAbilityRunner abilityRunner;

    protected virtual void Awake() {
        abilityRunner = GetComponent<ActiveAbilityRunner>();
    }
    protected virtual void OnEnable() {
        abilityRunner.RegisterActiveAbility(this);
    }
    protected virtual void OnDisable() {
        abilityRunner.UnregisterActiveAbility(this);
    }
    public abstract void Execute(Vector2 aimVector);
}