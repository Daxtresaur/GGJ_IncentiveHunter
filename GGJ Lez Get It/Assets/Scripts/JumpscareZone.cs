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

    private Animator _animator;

    void Start()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        collider.enabled = false;
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("Timer" + timer);
        
        if (timer >= 3.0f && timer < 5.0f)
        {
            collider.enabled = true;
            _animator.Play("RootGrow");
        }
        else if (timer< 3.0f)
        {
            _animator.Play("RootIdle");
        }
        else if (timer >= 4.0f && timer <= 7.0f)
        {
            _animator.Play("RootIdleGrown");
            if (inZone && health != null)
            {
                if (damageActive)
                {
                    //_animator.CrossFade("RootAttack", 0.5f);
                    health.Damage(damage);
                    damageActive = false;
                    _animator.Play("RootAttack");
                    //Debug.Log("damage");
                }

            }
            //Debug.Log(2);
        }
        else if (timer > 9.0f)
        {
            _animator.Play("RootUngrow");
            timer = 0.0f;
            collider.enabled = false;
            damageActive = true;
            //Debug.Log(3);
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
