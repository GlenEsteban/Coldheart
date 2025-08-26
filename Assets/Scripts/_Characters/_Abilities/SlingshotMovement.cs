using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SlingshotMovement : MonoBehaviour {
    [SerializeField, Range(1, 100)] private float _movementSpeed = 33f;

    private Rigidbody2D _rigidBody2D;

    private bool _isMoving = false;
    private Vector2 _moveVelocity = Vector2.zero;
    private Camera _camera;

    private void Awake() {
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();

        _camera = Camera.main;
    }

    private void OnEnable() {
        PlayerInputManager.Instance.OnClickStart += StartMovementCalculation;
        PlayerInputManager.Instance.OnClickEnd += ExecuteMovement;
    }

    private void OnDisable() {
        PlayerInputManager.Instance.OnClickStart -= StartMovementCalculation;
        PlayerInputManager.Instance.OnClickEnd -= ExecuteMovement;
    }

    private void Update() {
        if (_isMoving) {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _moveVelocity = gameObject.transform.position - mousePosition;
        }
    }

    private void MoveTowards(Vector2 velocity) {
        _rigidBody2D.AddForce(velocity * _movementSpeed);
    }

    private void StartMovementCalculation() {
        if (PlayerInputManager.Instance.SelectedCharacterOnClickStart != gameObject.GetComponent<Character>()) {
            _moveVelocity = Vector2.zero;
            _isMoving = false; 
            return;
        }

        _isMoving = true;
    }

    private void ExecuteMovement() {
        // Move only if dragging away from character
        if (PlayerInputManager.Instance.SelectedCharacterOnClickStart != gameObject.GetComponent<Character>() ||
            PlayerInputManager.Instance.SelectedCharacterOnClickEnd == gameObject.GetComponent<Character>()) {
            _moveVelocity = Vector2.zero;
            _isMoving = false;
            return;
        }

        MoveTowards(_moveVelocity);

        _moveVelocity = Vector2.zero;
        _isMoving = false;
    }
}
