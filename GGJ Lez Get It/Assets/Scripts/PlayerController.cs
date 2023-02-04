using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 3.0f;
    public float InitialSpeed { get { return initialSpeed; } }
    public float Speed;

    private Vector2 direction;
    //private Rigidbody rb;
    Vector3 direction3D = new();

    #region States
    PlayerState currentState;
    public PlayerAliveState AliveState { get; private set; } = new();
    public PlayerDeadState DeadState { get; private set; } = new();
    #endregion

    public HealthComponent HP { get; private set; }

    public void SetState(PlayerState state)
    {
        if (state == currentState) return;

        //Do all exit actions
        currentState?.OnExit(this);
        //Set current state
        currentState = state ?? AliveState;
        //Set initial state
        currentState.OnStart(this);
    }

    public static PlayerController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //rb = GetComponent<Rigidbody>();
        HP = GetComponent<HealthComponent>();

        Speed = initialSpeed;
    }

    private void Start()
    {
        SetState(AliveState);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>().normalized;
    }

    #region Updates
    public void Update()
    {
        currentState?.OnUpdate(this);
    }
    private void FixedUpdate()
    {
        currentState?.OnFixedUpdate(this);
    }
    #endregion

    #region PlayerMethods
    public void MovePlayer()
    {
        direction3D.x = direction.x;
        direction3D.y = 0.0f;
        direction3D.z = direction.y;

        transform.position += Speed * Time.fixedDeltaTime * direction3D;
    }

    public void SelfDestruct()
    {
        Camera.main.transform.SetParent(null);
        Destroy(gameObject);
    }
    #endregion

    #region Events
    private void OnEnable()
    {
        HP.OnDie += OnDie;
    }
    private void OnDisable()
    {
        HP.OnDie -= OnDie;
    }

    public void OnDie()
    {
        SetState(DeadState);
    }
    #endregion
}

public abstract class PlayerState
{
    public virtual void OnStart(PlayerController controller) { }
    public virtual void OnUpdate(PlayerController controller) { }
    public virtual void OnFixedUpdate(PlayerController controller) { }
    public virtual void OnExit(PlayerController controller) { }
}

public class PlayerAliveState : PlayerState
{
    public override void OnFixedUpdate(PlayerController controller)
    {
        base.OnFixedUpdate(controller);
        controller.MovePlayer();
    }
}

public class PlayerDeadState : PlayerState
{
    public override void OnStart(PlayerController controller)
    {
        controller.SelfDestruct();
    }
}


