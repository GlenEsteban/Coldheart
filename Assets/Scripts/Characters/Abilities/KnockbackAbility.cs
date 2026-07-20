using UnityEngine;

public class KnockbackAbility : MonoBehaviour {
    [SerializeField] float knockbackMultiplier = 100f;

    private Character character;
    private Actions actions;
    private ColliderInteractions colliderInteractions;

    protected void Awake() {
        character = GetComponent<Character>();

        actions = character.Actions;
        colliderInteractions = character.ColliderInteractions; 
    }
    private void OnEnable() {
        colliderInteractions.OnEnemyCollision += Execute;
    }
    private void OnDisable() {
        colliderInteractions.OnEnemyCollision -= Execute;
    }
    public void Execute(Character character) {
        if (!CharacterManager.Instance.CheckIfSelectedCharacter(gameObject)) { return; }

        Movement collidedCharacterMovement = character.Movement;
        
        Vector2 knockbackVector = actions.AimVector;

        collidedCharacterMovement.Move(knockbackVector * knockbackMultiplier);
    }
}
