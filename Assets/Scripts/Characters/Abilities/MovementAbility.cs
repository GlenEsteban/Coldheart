using UnityEngine;

public class MovementAbility : MonoBehaviour {
    private AbilityRunner abilityRunner;
    private Movement movement;

    protected void Awake() {
        abilityRunner = GetComponent<AbilityRunner>();

        movement = GetComponent<Movement>();
    }
    private void OnEnable() {
        abilityRunner.OnMove += Execute;
    }
    private void OnDisable() {
        abilityRunner.OnMove -= Execute;
    }
    public void Execute(Vector2 aimVector) {
        movement.Move(aimVector);
    }
}