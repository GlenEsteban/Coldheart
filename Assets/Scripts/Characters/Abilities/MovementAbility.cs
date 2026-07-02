using UnityEngine;

public class MovementAbility : ActiveAbility {
    [SerializeField] private AbilityType abilityType;    
    public override void Execute(Vector2 aimVector) {
        Debug.Log("Executing movement " + aimVector);
    }
}