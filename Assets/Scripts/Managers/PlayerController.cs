using System;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour{
    private PlayerInputActions playerInputActions;
    private Camera mainCam;
    private Character selectedPlayerCharacter;

    private Vector2 currentMouseWorldPosition;
    private Vector2 aimVector;

    private bool isEngagingPrimaryAction = false;
    private bool isInvertingAimVector = false;

    private bool IsHoveringOverSelectedPlayer() {
        if (selectedPlayerCharacter == null) { return false; }

        bool isHoveringOverSelectedPlayer = selectedPlayerCharacter.HitCollider.OverlapPoint(currentMouseWorldPosition);

        return isHoveringOverSelectedPlayer;
    }
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        mainCam = Camera.main;
    }
    private void OnEnable() {
        playerInputActions.Enable();

        playerInputActions.Player.OnPrimaryAction.started += OnPrimaryAction;
        playerInputActions.Player.OnPrimaryAction.canceled += OnPrimaryAction;
        playerInputActions.Player.OnSecondaryAction.started += OnSecondaryAction;
        playerInputActions.Player.OnSecondaryAction.canceled += OnSecondaryAction;
    }
    private void OnDisable() {
        playerInputActions.Disable();

        playerInputActions.Player.OnPrimaryAction.started -= OnPrimaryAction;
        playerInputActions.Player.OnPrimaryAction.canceled -= OnPrimaryAction;
        playerInputActions.Player.OnSecondaryAction.started -= OnSecondaryAction;
        playerInputActions.Player.OnSecondaryAction.canceled -= OnSecondaryAction;
    }
    private void Update() {
        UpdateMousePosition();

        if (selectedPlayerCharacter == null) { return; }

        UpdateAimVector();

        selectedPlayerCharacter.ActiveAbilityRunner.SetAimVector(aimVector);
    }
    private void UpdateMousePosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        currentMouseWorldPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }
    private void UpdateAimVector() {
        // Calculate aim vector while engaging with primary action input interaction
        if (isEngagingPrimaryAction) {
            aimVector = (Vector2) selectedPlayerCharacter.transform.position - currentMouseWorldPosition;

            // Invert aiming mechanic
            if (isInvertingAimVector) {
                aimVector *= -1;
            }
        }

        // Nullify aim vector if hovering over selected charcater
        if (IsHoveringOverSelectedPlayer()) {
            aimVector = Vector2.zero;
        }
    }
    public void OnPrimaryAction(InputAction.CallbackContext context) {
        if (context.started) {
            isEngagingPrimaryAction = true;

            Vector2 positionOnPrimaryActionStart = currentMouseWorldPosition;    

            // Check for player characters on input start and sets Character Manager seleceted character
            Collider2D[] collidersOnPrimaryActionStart = Physics2D.OverlapPointAll(positionOnPrimaryActionStart);

            selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnPrimaryActionStart); 

            CharacterManager.Instance.SetSelectedCharacter(selectedPlayerCharacter); // sets to null if none are found
        }
        else if (context.canceled) {
            isEngagingPrimaryAction = false;

            if (IsHoveringOverSelectedPlayer()) {
                // Display abilities when clicking on a character
            }
            else if (selectedPlayerCharacter != null) {
                // Execute selected character's active ability
                ActiveAbilityRunner selectedCharacterAbilityRunner = CharacterManager.Instance.SelectedCharacter.ActiveAbilityRunner;

                if (selectedCharacterAbilityRunner == null) { return; }

                selectedCharacterAbilityRunner.ExecuteActiveAbility();
            }

            aimVector = Vector2.zero;
        }
    }
    private Character CheckForPlayerCharacter(Collider2D[] colliderArrayToCheck) {
        if (colliderArrayToCheck == null) { return null; }

        foreach (Collider2D collider in colliderArrayToCheck) {
            if (collider.isTrigger) { continue; }

            Character selectedCharacter = collider.transform.GetComponentInParent<Character>();

            if (selectedCharacter != null && selectedCharacter.CharacterType == CharacterType.Player) {
                return selectedCharacter;
            }
        }

        return null;
    }
    public void OnSecondaryAction(InputAction.CallbackContext context) {
        if (context.canceled) {
            isInvertingAimVector = !isInvertingAimVector;
        }
    }
}