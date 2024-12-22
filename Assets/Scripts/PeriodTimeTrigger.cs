using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodTimeTrigger : MonoBehaviour
{
    public int damageAmount = 20; // Количество урона, наносимого ловушкой
    public float activationInterval = 3.0f; // Интервал между активациями ловушки
    public Animator animator; // Ссылка на аниматор

    private void Start()
    {
        StartCoroutine(ActivateTrapPeriodically());
    }

    private IEnumerator ActivateTrapPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);
            ApplyTrapDamage(); // Нанесение урона после завершения анимации
        }
    }

    private void ApplyTrapDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (Collider collider in colliders)
        {
            CharacterStats characterStats = collider.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                characterStats.TakeDamage(damageAmount);
                Debug.Log("Персонаж получил урон от ловушки: " + damageAmount);
            }
        }
    }
}
