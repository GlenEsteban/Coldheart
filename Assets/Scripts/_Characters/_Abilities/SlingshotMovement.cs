using UnityEngine;
public class SlingshotMovement : MonoBehaviour {
    [SerializeField, Range(1, 100)] private float _movementSpeed = 33f;

    private Rigidbody2D _rigidBody2D;



    private void Awake() {
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();
    }

    private void OnEnable() {
        PlayerInputManager.Instance.OnClickStart += CheckForMovementInput;
    }

    private void OnDisable() {
        PlayerInputManager.Instance.OnClickStart -= CheckForMovementInput;
    }

    private void MoveInDirection(Vector2 direction) {
        _rigidBody2D.AddForce(direction * _movementSpeed);
    }



    private void CheckForMovementInput() {

    }
}
