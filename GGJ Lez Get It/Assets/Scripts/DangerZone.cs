using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageRate = 0.1f;
    private bool inZone;
    private HealthComponent health;
    public bool InZone
    {
        set
        {
            inZone = value;
            StopCoroutine(DamageOverTime());
            if (inZone)
            {
                StartCoroutine(DamageOverTime());
            }
            else
            {
                StopCoroutine(DamageOverTime());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (other.TryGetComponent(out HealthComponent HP))
        {
            health = HP;
            InZone = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (other.TryGetComponent(out PlayerController controller))
        {
            controller.Speed = controller.SlowSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = null;
            InZone = false;
        }

        if (other.TryGetComponent(out PlayerController controller))
        {
            controller.Speed = controller.WalkSpeed;
        }
    }

    IEnumerator DamageOverTime()
    {
        //if (inZone) yield break;
        float timer = 0.0f;
        while (inZone && health != null)
        {
            if(timer >= damageRate)
            {
                timer = 0.0f;
                health.Damage(damage);
            }
            else
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
        yield return null;
    }
}
