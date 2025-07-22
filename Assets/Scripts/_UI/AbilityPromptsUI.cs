using UnityEngine;

public class AbilityPromptsUI : MonoBehaviour {
    public void AddAbilityPromptUI(GameObject abilitypromptUI) {
        abilitypromptUI.transform.SetParent(transform, worldPositionStays: false);
    }
}
