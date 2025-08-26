using UnityEngine;

public interface IAbility{
    void DisplayAbilityExecutionPrompt();
    void Execute(GameObject user);
}