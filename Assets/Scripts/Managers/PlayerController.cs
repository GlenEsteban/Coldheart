using System;
using System.Collections.Generic;
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

        bool isHoveringOverSelectedPlayer = selectedPlayerCharacter.HitCollider.OverlapPoint(currentMouseWorldPosition);

        return isHoveringOverSelectedPlayer;
    }
    private bool IsHoveringOverEnemy() {
        List<Character> enemies = CharacterManager.Instance.EnemyCharacters;

        foreach (Character enemy in enemies) {
            bool isHoveringOverEnemy = enemy.HitCollider.OverlapPoint(currentMouseWorldPosition);

            if (isHoveringOverEnemy) {
                return true;
            }
        }

        return false;
    }

    private bool IsAimingAtEnemyInRange() {


        return false;
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

        selectedPlayerCharacter.AbilityRunner.SetAimVector(aimVector);
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
        }

        // Nullify aim vector if hovering over selected charcater
        if (IsHoveringOverSelectedPlayer()) {
            aimVector = Vector2.zero;
        }
    }
    public void OnPrimaryAction(InputAction.CallbackContext context) {
        if (context.started) {
            isEngagingPrimaryAction = true;

            // Check for player characters on input start and sets Character Manager seleceted character
            Collider2D[] collidersOnPrimaryActionStart = Physics2D.OverlapPointAll(currentMouseWorldPosition);

            selectedPlayerCharacter = CheckForPlayerCharacter(collidersOnPrimaryActionStart); 

            CharacterManager.Instance.SetSelectedCharacter(selectedPlayerCharacter); // sets to null if none are found
        }
        else if (context.canceled) {
            isEngagingPrimaryAction = false;

            if (selectedPlayerCharacter != null) {
                if (IsHoveringOverSelectedPlayer()) {
                    // Display abilities when clicking on a character
                }
                else if (IsHoveringOverEnemy()) {
                    Debug.Log("Attack");
                }
                else {
                    // Execute selected character's active ability
                    AbilityRunner selectedCharacterAbilityRunner = CharacterManager.Instance.SelectedCharacter.AbilityRunner;

                    if (selectedCharacterAbilityRunner == null) { return; }

                    selectedCharacterAbilityRunner.ExecuteAbilitiesOnMoveEvent();
                }
            }

            aimVector = Vector2.zero;
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

            AbilityRunner selectedCharacterAbilityRunner = CharacterManager.Instance.SelectedCharacter.AbilityRunner;

            if (selectedCharacterAbilityRunner == null) { return; }

            selectedCharacterAbilityRunner.ExecuteAbilitiesOnGuardEvent();
        }
    }
    public void OnTertiaryAction(InputAction.CallbackContext context) {
        if (context.performed) {
            if (CharacterManager.Instance.SelectedCharacter == null) { return; }

            AbilityRunner selectedCharacterAbilityRunner = CharacterManager.Instance.SelectedCharacter.AbilityRunner;

            if (selectedCharacterAbilityRunner == null) { return; }

            selectedCharacterAbilityRunner.ExecuteAbilitiesOnRestEvent();
        }
    }
}