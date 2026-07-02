using System;
using UnityEngine;

public class AbilityRunner : MonoBehaviour {
    public event Action<Vector2> OnExecuteActiveAbility;

    public void ExecuteActiveAbility(Vector2 aimVector) {
        OnExecuteActiveAbility?.Invoke(aimVector);
    }
}