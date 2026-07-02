using System;
using System.Collections.Generic;
using UnityEngine;
public class ActiveAbilityRunner : MonoBehaviour {
    public ActiveAbility SelectedActiveAbility { get; private set; }
    public IReadOnlyList<ActiveAbility> ActiveAbilities => activeAbilities;

    [SerializeField] private List<ActiveAbility> activeAbilities = new List<ActiveAbility>();

    public void Start() {
        SelectedActiveAbility = ActiveAbilities[0];
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
    public void ExecuteActiveAbility(Vector2 aimVector) {
        SelectedActiveAbility.Execute(aimVector);
    }
}