using System;
using UnityEngine;

public class ColliderInteractions : MonoBehaviour {
    [field: SerializeField] public Collider2D Collider { get; private set; }

    [SerializeField] private AbilityRunner abilityRunner;

    private void OnCollisionEnter2D(Collision2D collider) {
        Character collidedCharacter = collider.gameObject.GetComponent<Character>();

        if (collidedCharacter == null) { return; }
        
        if (!CharacterManager.Instance.CheckIfSelectedCharacter(gameObject)) { return; }

        if (collidedCharacter.CharacterType == CharacterType.Enemy) {
            abilityRunner.ExecuteAbilitiesOnEnemyCollision(collidedCharacter);

            collidedCharacter.Health.TakeDamage(10);
        }
        else if (collidedCharacter.CharacterType == CharacterType.Player) {
            abilityRunner.ExecuteAbilitiesOnPlayerCollision(collidedCharacter);
        }
    }
}