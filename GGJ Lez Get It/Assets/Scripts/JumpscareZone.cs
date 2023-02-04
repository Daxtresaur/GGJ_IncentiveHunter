using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareZone : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private bool inZone;
    private HealthComponent health;

    private SphereCollider collider;

    private bool damageActive = true;
    float timer = 0;

    void Start()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("Timer" + timer);

        if (timer >= 3.0f && timer < 4.0f)
        {
            collider.enabled = true;
            Debug.Log(1);
        }
        else if (timer >= 4.0f && timer <= 6.0f)
        {
            if (inZone && health != null)
            {
                if (damageActive)
                {
                    health.Damage(damage);
                    damageActive = false;
                    Debug.Log("damage");
                }

            }
            Debug.Log(2);
        }
        else if (timer > 6)
        {
            timer = 0.0f;
            collider.enabled = false;
            damageActive = true;
            Debug.Log(3);
        }

    }

    public bool InZone
    {
        set
        {
            inZone = value;
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

        if (other.TryGetComponent(out PlayerController controller))
        {
            controller.Speed = controller.InitialSpeed / 2.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = null;
            InZone = false;
        }
    }

    IEnumerator BurstDamage()
    {
        //if (inZone) yield break;
        float timer = 0.0f;
        bool once = true;
        while (inZone && health != null)
        {
            if (once)
            {
                health.Damage(damage);
                once = false;
            }
            yield return null;
        }
        yield return null;
    }

}
