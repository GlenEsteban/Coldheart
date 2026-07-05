using System;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour{
    private PlayerInputActions playerInputActions;
    private Camera mainCam;

    private Vector2 currentMouseWorldPosition;
    private Vector2 positionOnPrimaryActionStart;
    private Vector2 aimVector;

    private bool isEngagingPrimaryAction = false;
    private bool isEngagingSecondaryAction = false;

    private Character selectedPlayerCharacter;

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
    }
    private void UpdateMousePosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        currentMouseWorldPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }
    private void UpdateAimVector() {
        // Calculate aim vector while engaging with primary action input interaction
        if (isEngagingPrimaryAction) {
            aimVector = positionOnPrimaryActionStart - currentMouseWorldPosition;

            if (isEngagingSecondaryAction) {
                aimVector *= -1;
            }
        }

        // Update aimVectorIndicator based on input interactions
        AimVectorIndicator aimVectorIndicator = selectedPlayerCharacter.AimVectorIndicator;

        if (aimVectorIndicator == null) { return; }

        if (isEngagingPrimaryAction) {
            aimVectorIndicator.Display();

            aimVectorIndicator.SetAimVector(aimVector);
        }
        else {
            aimVectorIndicator.Hide();
        }
    }
    public void OnPrimaryAction(InputAction.CallbackContext context) {
        if (context.started) {
            isEngagingPrimaryAction = true;

            positionOnPrimaryActionStart = currentMouseWorldPosition;

            // Check for player characters and update selected character
            Collider2D[] collidersOnPrimaryActionStart = Physics2D.OverlapPointAll(positionOnPrimaryActionStart);

            if (collidersOnPrimaryActionStart != null) {
                selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnPrimaryActionStart); // can be null

                CharacterManager.Instance.SetSelectedCharacter(selectedPlayerCharacter); 
            }
            else {
                CharacterManager.Instance.SetSelectedCharacter(null);
            }
        }
        else if (context.canceled) {
            isEngagingPrimaryAction = false;

            // Check for player characters on input canceled/released
            Collider2D[] collidersOnPrimaryActionEnd = Physics2D.OverlapPointAll(currentMouseWorldPosition);

            Character playerCharacterOnPrimaryActionEnd = CheckForPlayerCharacter(collidersOnPrimaryActionEnd);

            if (playerCharacterOnPrimaryActionEnd == selectedPlayerCharacter) {
                // Display abilities when clicking on a character

            }
            else if (selectedPlayerCharacter != null && 
                playerCharacterOnPrimaryActionEnd != selectedPlayerCharacter || collidersOnPrimaryActionEnd == null) {
                // Execute selected character's active ability
                ActiveAbilityRunner selectedCharacterAbilityRunner = CharacterManager.Instance.SelectedCharacter.ActiveAbilityRunner;

                if (selectedCharacterAbilityRunner == null) { return; }

                selectedCharacterAbilityRunner.ExecuteActiveAbility(aimVector);
            }
        }
    }
    private Character CheckForPlayerCharacter(Collider2D[] colliderArrayToCheck) {
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
            isEngagingSecondaryAction = !isEngagingSecondaryAction;
        }
    }
}