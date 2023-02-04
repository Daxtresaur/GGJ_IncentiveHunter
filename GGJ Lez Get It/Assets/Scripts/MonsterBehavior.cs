using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] FoundYouAudio;

    //[SerializeField] private float speed;
    [SerializeField] private float patrolSpeed = 1.0f;
    public float PatrolSpeed { get { return patrolSpeed; } }
    
    [SerializeField] private float chaseSpeed = 3.0f;
    public float ChaseSpeed { get { return chaseSpeed; } }
    [SerializeField] private Transform[] patrolPoints;
    private int index = 0;
    public NavMeshAgent Agent { get; private set; }
    public Transform Target; // get player position
    public FieldOfView FOV { get; private set; }

    #region State
    MonsterStates currentState;
    public PatrolState PatrolState { get; private set; } = new();
    public ChaseState ChaseState { get; private set; } = new();
    #endregion
    public void SetState(MonsterStates state)
    {
        Debug.Log(state);
        if (state == currentState) return;

        //Do all exit actions
        currentState?.OnExit(this);
        //Set current state
        currentState = state ?? PatrolState;
        //Set initial state
        currentState.OnStart(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        FOV = GetComponentInChildren<FieldOfView>();
        SetState(PatrolState);
        Teleport();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.OnUpdate(this);
    }

    public void Patrol()
    {
        //Debug.Log(Agent.remainingDistance);
        if (Agent.remainingDistance <= 0.2f) { index = Random.Range(0, patrolPoints.Length); };
        if (index >= patrolPoints.Length) index = 0;
        StartCoroutine(Move(patrolPoints[index].position));
    }

    public void Teleport()
    {
        Vector3 playerPos = PlayerController.instance.transform.position;
        int tIndex = 0;
        float distance = Vector3.Distance(playerPos, patrolPoints[0].position);
        for(int i = 1; i < patrolPoints.Length; i++)
        {

            Vector3 patrolPos = patrolPoints[i].position;
            float tempDistance = Vector3.Distance(playerPos, patrolPoints[i].position);
            if (distance < tempDistance) { tIndex = i; }
        }

        transform.position = patrolPoints[tIndex].position;
    }

    IEnumerator Move(Vector3 pos)
    {
        yield return null;
        Agent.SetDestination(pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        LightBehavior lb = other.GetComponentInChildren<LightBehavior>();
        //lb.KillLight();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent(out HealthComponent HP))
        {
            HP.Damage(1);
        }
    }
}

public class MonsterStates
{
    public virtual void OnStart(MonsterBehavior behavior) { }
    public virtual void OnUpdate(MonsterBehavior behavior) { }
    public virtual void OnLateUpdate(MonsterBehavior behavior) { }
    public virtual void OnExit(MonsterBehavior behavior) { }
}

public class PatrolState : MonsterStates
{
    public override void OnStart(MonsterBehavior behavior)
    {
        base.OnStart(behavior);
        behavior.Agent.speed = behavior.PatrolSpeed;
    }
    public override void OnUpdate(MonsterBehavior behavior)
    {
        base.OnUpdate(behavior);
        behavior.Patrol();
        if (behavior.FOV.visibleTargets.Count <= 0) return;

        behavior.SetState(behavior.ChaseState);

    }
}

public class ChaseState : MonsterStates 
{
    public override void OnStart(MonsterBehavior behavior)
    {
        base.OnStart(behavior);
        AudioClip[] clips = behavior.FoundYouAudio;
        int RandomRange = Random.Range(0, clips.Length);
        SoundManager.instance.PlaySFX(clips[RandomRange]);
        behavior.Agent.speed = behavior.ChaseSpeed;
    }

    public override void OnUpdate(MonsterBehavior behavior)
    {
        base.OnUpdate(behavior);
        //Set to patrol
        behavior.StartCoroutine(FindTarget(behavior));
    }

    public override void OnExit(MonsterBehavior behavior)
    {
        base.OnExit(behavior);
        //behavior.Teleport();
    }

    IEnumerator FindTarget(MonsterBehavior behavior)
    {
        yield return null;
        if (behavior.FOV.visibleTargets.Count <= 0) 
        { 
            behavior.SetState(behavior.PatrolState); 
            yield break;
        }

        if (behavior.FOV.visibleTargets[0] == null) yield break;
        behavior.Agent.SetDestination(behavior.FOV.visibleTargets[0].position);
    }
}
