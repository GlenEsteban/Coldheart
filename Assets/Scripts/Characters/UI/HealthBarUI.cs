using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour {
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageBar;
    [SerializeField] private float healthBarLerpDuration = 0.1f;
    [SerializeField] private float damageBarLerpDuration = 0.1f;
    [SerializeField] private float damageBarDelayBeforeLerp = 0.1f;

    private Coroutine lerpingHealthBarFillAmount;
    private Coroutine lerpingDamageBarFillAmount;

    private void OnEnable() {
        health.OnHealthChanged += UpdateHealthBarFill;
    }
    private void OnDisable() {
        health.OnHealthChanged -= UpdateHealthBarFill;
    }

    private void UpdateHealthBarFill() {
        if (lerpingHealthBarFillAmount != null) {
            StopCoroutine(lerpingHealthBarFillAmount);
        }
        if (lerpingDamageBarFillAmount != null) {
            StopCoroutine(lerpingDamageBarFillAmount);
        }

        float targetAmount = (float) health.CurrentHealth / health.MaxHealth;

        lerpingHealthBarFillAmount = StartCoroutine(LerpHealthBarFillAmount(targetAmount));
        lerpingDamageBarFillAmount = StartCoroutine(LerpDamageBarFillAmount(targetAmount));
    }

    private IEnumerator LerpHealthBarFillAmount(float targetAmount) {
        float startAmount = healthBar.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < healthBarLerpDuration) {
            elapsedTime += Time.deltaTime;

            healthBar.fillAmount = Mathf.Lerp(startAmount, targetAmount, elapsedTime / healthBarLerpDuration);

            yield return null;
        }

        healthBar.fillAmount = targetAmount;
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
