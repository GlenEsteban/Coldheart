using UnityEngine;

public class Movement : MonoBehaviour {
    private const float MOVEMENT_SPEED_ADJUSTMENT_CONSTANT = 250f;

    private Rigidbody2D rb;

    private float weightCompensationScalar = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        weightCompensationScalar = rb.mass;
    }
    public void Move(Vector2 forceVector) {
        rb.AddForce(forceVector * MOVEMENT_SPEED_ADJUSTMENT_CONSTANT * weightCompensationScalar);
    }
}