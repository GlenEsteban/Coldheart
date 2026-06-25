using System;
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

    private PlayerInputActions playerInputActions;
    private Camera mainCam;

    private Vector2 mouseClickPoint;

    private void Awake() {
        playerInputActions = new PlayerInputActions();

        mainCam = Camera.main;

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    private void OnEnable() {
        playerInputActions.Enable();

        playerInputActions.Player.OnMouseClick.started += OnMouseClick;
        playerInputActions.Player.OnMouseClick.canceled += OnMouseClick;
    }

    private void OnDisable() {
        playerInputActions.Disable();

        playerInputActions.Player.OnMouseClick.started -= OnMouseClick;
        playerInputActions.Player.OnMouseClick.canceled -= OnMouseClick;
    }

    public void OnMouseClick(InputAction.CallbackContext context) {
        UpdateMousePosition();

        if (context.started) {
            CollidersOnClickStart = Physics2D.OverlapPointAll(mouseClickPoint);

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
            CollidersOnClickEnd = Physics2D.OverlapPointAll(mouseClickPoint);
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
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        mouseClickPoint = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
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
}