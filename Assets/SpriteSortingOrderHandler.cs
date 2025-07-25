using UnityEngine;

public class SpriteSortingOrderHandler : MonoBehaviour {
    private static int sortingOrderBase = 5000;
    [SerializeField] private int _orderSpacingFactor = 100;
    [SerializeField] private int _offset = 0;

    private SpriteRenderer _spriteRenderer;
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        CalculateAndSetSortingOrder();

        if (gameObject.isStatic) { enabled = false; } // Disable component if static after calculating sorting order
    }

    private void CalculateAndSetSortingOrder() {
        _spriteRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y * _orderSpacingFactor)) + _offset;
    }
}
