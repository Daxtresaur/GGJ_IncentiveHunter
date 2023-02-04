using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    //[SerializeField] private float speed;
    [SerializeField] private Transform[] patrolPoints;
    private int index = 0;
    public NavMeshAgent agent { get; private set; }
    public Transform target; // get player position
    public FieldOfView FOV { get; private set; }

    MonsterStates currentState;
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public void SetState(MonsterStates state)
    {
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
        agent = GetComponent<NavMeshAgent>();
        FOV = GetComponentInChildren<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.OnUpdate(this);
    }

    public void Patrol()
    {
        if(agent.pathStatus == NavMeshPathStatus.PathComplete) index++;
        if (index <= patrolPoints.Length) index = 0;
        agent.SetDestination(patrolPoints[index].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        LightBehavior lb = other.GetComponentInChildren<LightBehavior>();
        lb.KillLight();
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
    public override void OnUpdate(MonsterBehavior behavior)
    {
        base.OnUpdate(behavior);
        behavior.Patrol();

    }
    public override void OnLateUpdate(MonsterBehavior behavior)
    {
        if (behavior.FOV.visibleTargets.Count <= 0) return;

        behavior.SetState(behavior.PatrolState);
    }
}

public class ChaseState : MonsterStates 
{
    public override void OnUpdate(MonsterBehavior behavior)
    {
        base.OnLateUpdate(behavior);
        //Set to patrol
        if (behavior.FOV.visibleTargets.Count <= 0) behavior.SetState(behavior.PatrolState);

        behavior.StartCoroutine(FindTarget(behavior));
    }

    IEnumerator FindTarget(MonsterBehavior behavior)
    {
        yield return null;
        behavior.agent.SetDestination(behavior.FOV.visibleTargets[0].position);
    }
}
