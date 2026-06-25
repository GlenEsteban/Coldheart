using UnityEngine;

public class SpriteSortingOrderUpdater : MonoBehaviour {
    private static int sortingOrderBase = 5000;

    [SerializeField] private int orderSpacingFactor = 100;
    [SerializeField] private int offset = 0;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        CalculateAndSetSortingOrder();

        // Disable component if static after calculating sorting order
        if (gameObject.isStatic) { 
            enabled = false;
        } 
    }

    private void CalculateAndSetSortingOrder() {
        spriteRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y * orderSpacingFactor)) + offset;
    }
}
