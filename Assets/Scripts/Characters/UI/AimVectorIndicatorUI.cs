using UnityEngine;

public class AimVectorIndicatorUI : MonoBehaviour {
    [SerializeField] private float angleOffset = -90f;
    [SerializeField] private float indicatorWidth = -0.25f;

    private Actions actions;
    private RectTransform indicatorRectTransform;
    private SpriteRenderer indicatorSpriteRenderer;

    private Vector2 aimVector;

    private void Awake() {
        actions = GetComponentInParent<Actions>();
        indicatorRectTransform = GetComponent<RectTransform>();
        indicatorSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        aimVector = actions.AimVector;

        UpdateVisibility();

        UpdateIndicatorRotation();

        UpdateIndicatorScale();
    }
    private void UpdateVisibility() {
        if (actions.IsAiming) {
            indicatorSpriteRenderer.enabled = true;
        }
        else {
            indicatorSpriteRenderer.enabled = false; 
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