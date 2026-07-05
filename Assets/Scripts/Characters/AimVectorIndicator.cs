using UnityEngine;

public class AimVectorIndicator : MonoBehaviour {
    [SerializeField] private RectTransform IndicatorRectTransform;
    [SerializeField] private SpriteRenderer IndicatorSpriteRenderer;

    [SerializeField] private float angleOffset = -90f;
    [SerializeField] private float indicatorWidth = -0.25f;

    private Vector2 aimVector;
    public void SetAimVector(Vector2 vector) {
        aimVector = vector;
    }
    void Update() {
        UpdateIndicatorRotation();
        UpdateIndicatorScale();
    }
    private void UpdateIndicatorRotation() {
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

        IndicatorRectTransform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
    }    
    private void UpdateIndicatorScale() {
        IndicatorSpriteRenderer.size = new Vector2(indicatorWidth, aimVector.magnitude);
    }
    public void Display() {
        IndicatorSpriteRenderer.enabled = true;
    }    
    public void Hide() {
        IndicatorSpriteRenderer.enabled = false;
    }
}