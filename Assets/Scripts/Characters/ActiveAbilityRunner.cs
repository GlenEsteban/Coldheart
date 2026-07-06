using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveAbilityRunner : MonoBehaviour {
    public event Action<Vector2> OnExecuteActiveAbility;
    [field: SerializeField] public ActiveAbility SelectedActiveAbility { get; private set; }
    [field: SerializeField] public Vector2 AimVector { get; private set; }
    public IReadOnlyList<ActiveAbility> ActiveAbilities => activeAbilities;

    [SerializeField] private List<ActiveAbility> activeAbilities = new List<ActiveAbility>();

    public void SetAimVector(Vector2 vector) {
        Vector2 clampedVector = Vector2.ClampMagnitude(vector, SelectedActiveAbility.MaxAimMagnitude);

        AimVector = clampedVector;
    }
    public void Start() {
        if (ActiveAbilities.Count == 0) {
            ActiveAbility activeAbility = GetComponent<ActiveAbility>();
            SetSelectedActiveAbility(activeAbility);
        }
    }
    public void SetSelectedActiveAbility(ActiveAbility activeAbility) {
        SelectedActiveAbility = activeAbility;
    }
    public void RegisterActiveAbility(ActiveAbility activeAbility) {
        if (activeAbilities.Contains(activeAbility)) { return; }

        activeAbilities.Add(activeAbility);
    }
    public void UnregisterActiveAbility(ActiveAbility activeAbility) {
        if (!activeAbilities.Contains(activeAbility)) { return; }

        activeAbilities.Remove(activeAbility);
    }
    public void ExecuteActiveAbility() {
        SelectedActiveAbility.Execute(AimVector);

        // Calls event for any subscribed passive abilities
        OnExecuteActiveAbility?.Invoke(AimVector);
    }
}