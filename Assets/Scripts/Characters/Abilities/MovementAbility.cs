using UnityEngine;

public class MovementAbility : MonoBehaviour {
    private Character character;
    private Actions actions;
    private Movement movement;

    protected void Awake() {
        character = GetComponent<Character>();

        actions = character.Actions; 
        movement = character.Movement;
    }
    private void OnEnable() {
        actions.OnMove += Execute;
    }
    private void OnDisable() {
        actions.OnMove -= Execute;
    }
    public void Execute(Vector2 forceVector) {
        movement.Move(forceVector);
    }
}