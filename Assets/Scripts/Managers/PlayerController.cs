using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    private PlayerInputActions playerInputActions;
    private Camera mainCam;
    private Character selectedPlayerCharacter;
    private Vector2 currentMouseWorldPosition;
    private Vector2 aimVector;

    private bool isEngagingPrimaryAction = false;

    private bool IsHoveringOverSelectedPlayer() {
        if (selectedPlayerCharacter == null) { return false; }

        bool isHoveringOverSelectedPlayer = selectedPlayerCharacter.ColliderInteractions.Collider.OverlapPoint(currentMouseWorldPosition);

        return isHoveringOverSelectedPlayer;
    }
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        mainCam = Camera.main;
    }
    private void OnEnable() {
        playerInputActions.Enable();

        playerInputActions.Player.MoveOrAttack.started += OnPrimaryAction;
        playerInputActions.Player.MoveOrAttack.canceled += OnPrimaryAction;
        playerInputActions.Player.Guard.performed += OnSecondaryAction;
        playerInputActions.Player.Rest.performed += OnTertiaryAction;
    }
    private void OnDisable() {
        playerInputActions.Disable();

        playerInputActions.Player.MoveOrAttack.started -= OnSecondaryAction;
        playerInputActions.Player.MoveOrAttack.canceled -= OnSecondaryAction;
        playerInputActions.Player.Guard.performed -= OnSecondaryAction;
        playerInputActions.Player.Rest.performed -= OnTertiaryAction;
    }
    private void Update() {
        UpdateMousePosition();

        if (selectedPlayerCharacter == null) { return; }

        UpdateAimVector();

        selectedPlayerCharacter.Actions.SetAimVector(aimVector);
    }
    private void UpdateMousePosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        currentMouseWorldPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }
    private void UpdateAimVector() {
        // Calculate aim vector while engaging with primary action input interaction
        if (isEngagingPrimaryAction) {
            aimVector = currentMouseWorldPosition - (Vector2) selectedPlayerCharacter.transform.position;
            selectedPlayerCharacter.Actions.SetIsAiming(true);
        }

        // Nullify aim vector if hovering over selected charcater
        if (IsHoveringOverSelectedPlayer()) {
            aimVector = Vector2.zero;
        }
    }
    public void OnPrimaryAction(InputAction.CallbackContext context) {
        if (context.started) {
            isEngagingPrimaryAction = true;
            
            Collider2D[] collidersOnPrimaryActionStart = Physics2D.OverlapPointAll(currentMouseWorldPosition);

            selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnPrimaryActionStart); 

            CharacterManager.Instance.SetSelectedCharacter(selectedPlayerCharacter); // sets to null if none are found
        }
        else if (context.canceled) {
            isEngagingPrimaryAction = false;

            if (selectedPlayerCharacter != null) {

                selectedPlayerCharacter.Actions.SetIsAiming(false);

                if (IsHoveringOverSelectedPlayer()) {
                    // Display abilities when clicking on a character

                }
                else {
                    Actions selectedCharacterActions = CharacterManager.Instance.SelectedCharacter.Actions;

                    if (selectedCharacterActions == null) { return; }

                    selectedCharacterActions.Move();
                }
            }
        }
    }
    private Character CheckForPlayerCharacter(Collider2D[] colliderArrayToCheck) {
        if (colliderArrayToCheck == null) { return null; }

        foreach (Collider2D collider in colliderArrayToCheck) {
            //if (collider.isTrigger) { continue; }

            Character selectedCharacter = collider.transform.GetComponentInParent<Character>();

            if (selectedCharacter != null && selectedCharacter.CharacterType == CharacterType.Player) {
                return selectedCharacter;
            }
        }

        return null;
    }
    public void OnSecondaryAction(InputAction.CallbackContext context) {
        if (context.performed) {            
            if (CharacterManager.Instance.SelectedCharacter == null) { return; }

            Actions selectedCharacterActions = CharacterManager.Instance.SelectedCharacter.Actions;

            if (selectedCharacterActions == null) { return; }

            selectedCharacterActions.Guard();

            Debug.Log("Guard");
        }
    }
    public void OnTertiaryAction(InputAction.CallbackContext context) {
        if (context.performed) {
            if (CharacterManager.Instance.SelectedCharacter == null) { return; }

            Actions selectedCharacterActions = CharacterManager.Instance.SelectedCharacter.Actions;

            if (selectedCharacterActions == null) { return; }

            if (!IsHoveringOverSelectedPlayer()) { return; }

            selectedCharacterActions.Rest();

            Debug.Log("Rest");
        }
    }
}