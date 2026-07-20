using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour {
    [SerializeField] Color playerHealthBarColor;
    [SerializeField] Color enemyHealthBarColor;
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageBar;
    [SerializeField] private float damageBarDelayBeforeLerp = 0.1f;
    [SerializeField] private float damageBarLerpDuration = 0.1f;

    private Character character;
    private Coroutine lerpingDamageBarFillAmount;

    private float targetAmount;

    private void OnEnable() {
        health.OnDamageTaken += UpdateHealthBarFill;
    }
    private void OnDisable() {
        health.OnDamageTaken -= UpdateHealthBarFill;
    }
    private void Awake() {
        character = GetComponentInParent<Character>();
    }
    private void Start() {
        UpdateHealthBarColor();
    }
    private void UpdateHealthBarColor() {
        if (character.CharacterType == CharacterType.Player)
        {
            healthBar.color = playerHealthBarColor;
        }
        if (character.CharacterType == CharacterType.Enemy)
        {
            healthBar.color = enemyHealthBarColor;
        }
    }
    private void UpdateHealthBarFill(int currentHealth, int maxHealth) {
        if (lerpingDamageBarFillAmount != null) {
            StopCoroutine(lerpingDamageBarFillAmount);
        }

        targetAmount = (float) currentHealth / maxHealth;

        healthBar.fillAmount = targetAmount;

        lerpingDamageBarFillAmount = StartCoroutine(LerpDamageBarFillAmount(targetAmount));
    }
    private IEnumerator LerpDamageBarFillAmount(float targetAmount) {
        float startAmount = damageBar.fillAmount;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(damageBarDelayBeforeLerp);

        while (elapsedTime < damageBarLerpDuration) {
            elapsedTime += Time.deltaTime;

            damageBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, elapsedTime / damageBarLerpDuration);

            yield return null;
        }

        damageBar.fillAmount = targetAmount;
    }
}
