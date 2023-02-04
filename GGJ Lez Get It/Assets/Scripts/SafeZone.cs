using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    //private bool inZone;
    public float shrinkRate = 2.0f;
    private Light sLight;
    private SphereCollider col;
    private LightBehavior lb;
    private bool isDying = false;
#if false
    private bool isDying;
    public bool isDying
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
#endif

    private void Awake()
    {
        sLight = GetComponent<Light>();
        col = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        lb = other.GetComponentInChildren<LightBehavior>();
        lb.IncreaseLight();
        //StartCoroutine(DecreaseSafe());

        if (other.TryGetComponent(out PlayerController player))
        {
            player.Speed = player.InitialSpeed;
        }
        if (other.TryGetComponent(out HealthComponent HP))
        {
            HP.Heal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if(other.TryGetComponent(out PlayerController player))
        {
            player.Speed = player.InitialSpeed;
        }
        lb = null;
    }

#if false
    IEnumerator DecreaseSafe()
    {
        if (isDying) { yield break; }
        isDying = true;
        //if (inZone) yield break;
        float initialLightSize = sLight.spotAngle;
        float initColliderSize = col.radius;
        while (sLight.spotAngle > 0.0f)
        {
            sLight.spotAngle -= shrinkRate * Time.deltaTime;
            col.radius = initColliderSize * sLight.spotAngle / initialLightSize;
            yield return null;
        }
        sLight.spotAngle = 0.0f;
        col.radius = 0.0f;
        yield return null;
        isDying = false;
        gameObject.SetActive(false);
    }
#endif
}
