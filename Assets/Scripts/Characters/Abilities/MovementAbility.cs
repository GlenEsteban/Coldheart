using UnityEngine;

public class MovementAbility : ActiveAbility {
    [SerializeField] private AbilityType abilityType;

    private Movement movement;

    private void Awake() {
        movement = GetComponent<Movement>();
    }
    public override void Execute(Vector2 aimVector) {
        Debug.Log("Executing movement " + aimVector);

        movement.Move(aimVector);
    }
}