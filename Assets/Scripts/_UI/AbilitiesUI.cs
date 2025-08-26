using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour {
    [SerializeField] private List<Button> _abilityButtonsUI;
    [SerializeField] private float _radialDisplayRadius = 1.5f;

    private Animator _abilityButtonsUIAnimator;   
    
    public void AddAbliltyButtonUI(Button abilityButtonUI) {
        _abilityButtonsUI.Add(abilityButtonUI);

        abilityButtonUI.transform.SetParent(_abilityButtonsUIAnimator.gameObject.transform);

        PositionAbilityButtons();
    }
    public void DisplayAbliltiesUI() {
        PositionAbilityButtons();

        _abilityButtonsUIAnimator.gameObject.SetActive(true);
        _abilityButtonsUIAnimator.SetBool("IsDisplayed", true);
    }
    public void HideAbilitiesUI() {
        if (_abilityButtonsUIAnimator == null || 
            _abilityButtonsUIAnimator.GetBool("IsDisplayed") == false) { return; }

        _abilityButtonsUIAnimator.SetBool("IsDisplayed", false);

        StartCoroutine(DelayDisableAbilitiesUI()); // FIX ANIMTATION TIMING
    }
    private IEnumerator DelayDisableAbilitiesUI() {
        StopAllCoroutines();
        yield return new WaitForSeconds(1f);
        _abilityButtonsUIAnimator.gameObject.SetActive(false);
    }

    private void Awake() {
        _abilityButtonsUIAnimator = GetComponentInChildren<Animator>(true); // true = includes inactive children in search
    }

    private void PositionAbilityButtons() {
        float angle; ;
        float rad;
        Vector2 positionOnUnitCircle;

        for (int i = 0; i < _abilityButtonsUI.Count; i++) {
            angle = i * (360f / _abilityButtonsUI.Count) - 90f; // Initial angle offset by -90 (top of circle)
            rad = -1 * angle * Mathf.Deg2Rad; // Negative angle increments clockwise
            positionOnUnitCircle = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector2 buttonPosition = (positionOnUnitCircle * _radialDisplayRadius);

            _abilityButtonsUI[i].GetComponent<RectTransform>().anchoredPosition = buttonPosition;
        }
    }
}