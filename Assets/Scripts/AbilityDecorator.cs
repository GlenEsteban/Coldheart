using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDecorator : IAbility {
    protected IAbility wrappedAbility;

    public AbilityDecorator(IAbility ability) {
        wrappedAbility = ability;
    }

    public void DisplayAbilityExecutionPrompt() {
    }

    public virtual void Execute(GameObject user) {
        wrappedAbility?.Execute(user);
    }
}
