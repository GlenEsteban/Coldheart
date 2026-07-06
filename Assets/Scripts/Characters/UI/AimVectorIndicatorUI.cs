using UnityEditor.AnimatedValues;
using UnityEngine;

public class AimVectorIndicatorUI : MonoBehaviour {
    [SerializeField] private float angleOffset = -90f;
    [SerializeField] private float indicatorWidth = -0.25f;

    private ActiveAbilityRunner activeAbilityRunner;
    private RectTransform indicatorRectTransform;
    private SpriteRenderer indicatorSpriteRenderer;

    private Vector2 aimVector;

    private void Awake() {
        activeAbilityRunner = GetComponentInParent<ActiveAbilityRunner>();
        indicatorRectTransform = GetComponent<RectTransform>();
        indicatorSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        aimVector = activeAbilityRunner.AimVector;

        UpdateVisibility();

        UpdateIndicatorRotation();

        UpdateIndicatorScale();
    }
    private void UpdateVisibility() {
        if (aimVector.magnitude <= 0) {
            indicatorSpriteRenderer.enabled = false;
        }
        else {
            indicatorSpriteRenderer.enabled = true; 
        }
    }
    private void UpdateIndicatorRotation() {
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

        indicatorRectTransform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
    }
    private void UpdateIndicatorScale() {
        indicatorSpriteRenderer.size = new Vector2(indicatorWidth, aimVector.magnitude);
    }
}
