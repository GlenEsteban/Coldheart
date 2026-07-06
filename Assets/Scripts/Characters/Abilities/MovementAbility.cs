using UnityEngine;

public class MovementAbility : ActiveAbility {
    [field: SerializeField] public AbilityType AbilityType { get; private set; }

    private Movement movement;

    protected override void Awake() {
        base.Awake();

        movement = GetComponent<Movement>();
    }
    public override void Execute(Vector2 aimVector) {
        movement.Move(aimVector);
    }
}