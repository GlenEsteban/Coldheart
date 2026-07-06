using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesRadialDisplay : MonoBehaviour {
    [SerializeField] private List<Button> abilityButtons;
    [SerializeField] private float radialDisplayRadius = 1.5f;

    private Animator abilityRadialDisplayAnimator;   
    
    public void AddAbliltyButtonUI(Button abilityButtonUI) {
        abilityButtons.Add(abilityButtonUI);

        abilityButtonUI.transform.SetParent(abilityRadialDisplayAnimator.gameObject.transform);

        PositionAbilityButtons();
    }
    public void DisplayAbliltiesUI() {
        PositionAbilityButtons();

        abilityRadialDisplayAnimator.gameObject.SetActive(true);
        abilityRadialDisplayAnimator.SetBool("IsDisplayed", true);
    }
    public void HideAbilitiesUI() {
        if (abilityRadialDisplayAnimator == null || 
            abilityRadialDisplayAnimator.GetBool("IsDisplayed") == false) { return; }

        abilityRadialDisplayAnimator.SetBool("IsDisplayed", false);

        StartCoroutine(DelayDisableAbilitiesUI()); // FIX ANIMTATION TIMING
    }
    private IEnumerator DelayDisableAbilitiesUI() {
        StopAllCoroutines();
        yield return new WaitForSeconds(1f);
        abilityRadialDisplayAnimator.gameObject.SetActive(false);
    }

    private void Awake() {
        abilityRadialDisplayAnimator = GetComponentInChildren<Animator>(true); // true = includes inactive children in search
    }

    private void PositionAbilityButtons() {
        float angle; ;
        float rad;
        Vector2 positionOnUnitCircle;

        for (int i = 0; i < abilityButtons.Count; i++) {
            angle = i * (360f / abilityButtons.Count) - 90f; // Initial angle offset by -90 (top of circle)
            rad = -1 * angle * Mathf.Deg2Rad; // Negative angle increments clockwise
            positionOnUnitCircle = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector2 buttonPosition = (positionOnUnitCircle * radialDisplayRadius);

            abilityButtons[i].GetComponent<RectTransform>().anchoredPosition = buttonPosition;
        }
    }
}