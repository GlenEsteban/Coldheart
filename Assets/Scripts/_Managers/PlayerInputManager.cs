using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour{
    public static PlayerInputManager Instance;

    public event Action OnClickStart;
    public event Action OnClickEnd;
    public event Action OnColliderClickStart;
    public event Action OnColliderClickEnd;

    public Collider2D[] CollidersOnClickStart { get; private set; }
    public Collider2D[] CollidersOnClickEnd { get; private set; }

    public Character SelectedCharacterOnClickStart { get; private set; }
    public Character SelectedCharacterOnClickEnd { get; private set; }

    private PlayerInputActions _playerInputActions;
    private Camera _mainCam;

    private Vector2 _mouseClickPoint;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();

        _mainCam = Camera.main;

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    private void OnEnable() {
        _playerInputActions.Enable();

        _playerInputActions.Player.OnMouseClick.started += OnMouseClick;
        _playerInputActions.Player.OnMouseClick.canceled += OnMouseClick;
    }

    private void OnDisable() {
        _playerInputActions.Disable();

        _playerInputActions.Player.OnMouseClick.started -= OnMouseClick;
        _playerInputActions.Player.OnMouseClick.canceled -= OnMouseClick;
    }

    public void OnMouseClick(InputAction.CallbackContext context) {
        UpdateMousePosition();

        if (context.started) {
            CollidersOnClickStart = Physics2D.OverlapPointAll(_mouseClickPoint);
            if (CollidersOnClickStart != null) {
                SelectedCharacterOnClickStart = CheckForCharacter(CollidersOnClickStart);

                OnColliderClickStart?.Invoke();
            }
            else {
                SelectedCharacterOnClickStart = null;
            }

            OnClickStart?.Invoke();
        }
        else if (context.canceled) {
            CollidersOnClickEnd = Physics2D.OverlapPointAll(_mouseClickPoint);
            if (CollidersOnClickEnd != null) {
                SelectedCharacterOnClickEnd= CheckForCharacter(CollidersOnClickEnd);

                OnColliderClickEnd?.Invoke();
            }
            else {
                SelectedCharacterOnClickEnd= null;
            }

            OnClickEnd?.Invoke();
        }
    }

    private void UpdateMousePosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = _mainCam.ScreenToWorldPoint(mouseScreenPos);
        _mouseClickPoint = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }

    public Character CheckForCharacter(Collider2D[] colliderArrayToCheck) {
        foreach (Collider2D collider in colliderArrayToCheck) {
            Character selectedCharacter = collider.transform.GetComponentInParent<Character>();

            if (selectedCharacter != null) {
                return selectedCharacter;
            }
        }

        return null;
    }

    public bool HasClickedOnTargetCollider(Collider2D targetCollider) {
        foreach (Collider2D collider in CollidersOnClickStart) {
            if (collider == targetCollider) {
                return true;
            }
        }

        return false;
    }
}