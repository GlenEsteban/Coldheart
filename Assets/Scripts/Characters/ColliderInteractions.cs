using System;
using UnityEngine;

public class ColliderInteractions : MonoBehaviour {
    public event Action<Character> OnEnemyCollision;
    public event Action<Character> OnPlayerCollision;
    [field: SerializeField] public Collider2D Collider { get; private set; }

    private void OnCollisionEnter2D(Collision2D collider) {
        Character collidedCharacter = collider.gameObject.GetComponent<Character>();

        if (collidedCharacter == null) { return; }
        
        if (collidedCharacter.CharacterType == CharacterType.Enemy) {
            OnEnemyCollision?.Invoke(collidedCharacter);

            collidedCharacter.Health.TakeDamage(10);
        }
        else if (collidedCharacter.CharacterType == CharacterType.Player) {
            OnPlayerCollision?.Invoke(collidedCharacter);
        }
    }
}