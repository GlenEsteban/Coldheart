using UnityEngine;

public class MovementAbility : MonoBehaviour {
    [SerializeField] private AbilityType abilityType;

    private AbilityRunner abilityRunner;

    private void Awake() {
        abilityRunner = GetComponent<AbilityRunner>();
    }
    private void OnEnable() {
        if (abilityRunner != null) {
            abilityRunner.OnExecuteActiveAbility += Execute;
        }
    }
    private void OnDisable() {
        if (abilityRunner != null) {
            abilityRunner.OnExecuteActiveAbility -= Execute;
        }
    }
    public void Execute(Vector2 aimVector) {
        Debug.Log("Executing movement " + aimVector);
    }
}
