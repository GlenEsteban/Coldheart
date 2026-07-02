using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    private PlayerInputActions playerInputActions;
    private Camera mainCam;

    private Vector2 currentMouseWorldPosition;
    private Vector2 positionOnClickStart;
    private Vector2 positionOnClickEnd;
    private Vector2 aimVector;

    private bool isEngagingControls = false;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        mainCam = Camera.main;
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
    private void Update() {
        UpdateMousePosition();

        UpdateAimVector();
    }
    private void UpdateMousePosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        currentMouseWorldPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }
    private void UpdateAimVector() {
        if (isEngagingControls) {
            aimVector = positionOnClickStart - currentMouseWorldPosition;
        }
        else {
            aimVector = positionOnClickStart - positionOnClickEnd;
        }
    }
    public void OnMouseClick(InputAction.CallbackContext context) {
        Character selectedPlayerCharacter;

        if (context.started) {
            isEngagingControls = true;

            positionOnClickStart = currentMouseWorldPosition;

            Collider2D[] collidersOnClickStart = Physics2D.OverlapPointAll(currentMouseWorldPosition);

            if (collidersOnClickStart != null) {
                selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnClickStart);

                CharacterManager.Instance.SetSelectedCharacter(selectedPlayerCharacter);
            }
            else {
                CharacterManager.Instance.SetSelectedCharacter(null);
            }
        }
        else if (context.canceled) {
            isEngagingControls = false;

            positionOnClickEnd = currentMouseWorldPosition;

            Collider2D[] collidersOnClickEnd = Physics2D.OverlapPointAll(currentMouseWorldPosition);

            selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnClickEnd);

            if (selectedPlayerCharacter == CharacterManager.Instance.SelectedCharacter) {
                // Display abilities when clicking on a character
            }
            else if (selectedPlayerCharacter != CharacterManager.Instance.SelectedCharacter || collidersOnClickEnd == null) {
                // Call AbilityRunner method to execute active ability when releasing aim 
                selectedPlayerCharacter = CharacterManager.Instance.SelectedCharacter;
                AbilityRunner characterAbilityRunner = selectedPlayerCharacter.GetComponent<AbilityRunner>();

                if (characterAbilityRunner == null) { return; }

                characterAbilityRunner.ExecuteActiveAbility(aimVector);
            }
        }
    }
    public Character CheckForPlayerCharacter(Collider2D[] colliderArrayToCheck) {
        foreach (Collider2D collider in colliderArrayToCheck) {
            if (collider.isTrigger) { continue; }

            Character selectedCharacter = collider.transform.GetComponentInParent<Character>();

            if (selectedCharacter != null && selectedCharacter.characterType == CharacterType.Player) {
                return selectedCharacter;
            }
        }

        return null;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;

        if (!Application.isPlaying) { return; }

        Character selectedCharacter = CharacterManager.Instance.SelectedCharacter;

        if (selectedCharacter == null) { return; }

        Vector2 selectedCharacterPosition = selectedCharacter.transform.position;

        Gizmos.DrawLine(selectedCharacterPosition, selectedCharacterPosition + aimVector);
    }
}