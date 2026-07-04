using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 100f;

    private Rigidbody2D rb;

    private float weightCompensationScalar = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        weightCompensationScalar = rb.mass;
    }
    public void Move(Vector2 forceVector) {
        rb.AddForce(forceVector * moveSpeed * weightCompensationScalar);
    }
}
