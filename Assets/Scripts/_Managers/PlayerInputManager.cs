using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    public static PlayerInputManager Instance;

    public event Action OnClick;
    public Collider2D[] ClickedColliders { get; private set; }

    private PlayerInputActions _playerInputActions;
    private Camera _mainCam;

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

        _playerInputActions.Player.OnMouseClick.performed += OnMouseClick;
    }

    private void OnDisable() {
        _playerInputActions.Disable();

        _playerInputActions.Player.OnMouseClick.performed -= OnMouseClick;
    }

    public void OnMouseClick(InputAction.CallbackContext context) {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = _mainCam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 mousePoint = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        ClickedColliders = Physics2D.OverlapPointAll(mousePoint);

        if (ClickedColliders.Length < 1) { // If nothing is clicked, omit checks and reset selcected character
            CharacterManager.Instance.UpdateSelectedCharacter(null);
            ClickedColliders = null;
            return;
        }
        else {
            OnClick?.Invoke(); 
        }

        CheckForCharacters();
    }

    public void CheckForCharacters() {
        // MINOR BUG: Player can select character overlapping current selected character's colliders    <<<
        foreach (Collider2D collider in ClickedColliders) {
            Character selectedCharacter = collider.transform.GetComponentInParent<Character>();
            if (selectedCharacter != null) {
                CharacterManager.Instance.UpdateSelectedCharacter(selectedCharacter);
                return;
            }
            else {
                CharacterManager.Instance.UpdateSelectedCharacter(null);
            }
        }
    }

    public bool HasClickedOnTargetCollider(Collider2D targetCollider) {
        foreach (Collider2D collider in ClickedColliders) {
            if (collider == targetCollider) {
                return true;
            }
        }
        return false;
    }
}