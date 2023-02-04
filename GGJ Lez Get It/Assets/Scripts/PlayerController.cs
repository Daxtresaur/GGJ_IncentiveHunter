using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementStates
{
    walking, running, slow
}
public class PlayerController : MonoBehaviour
{
    public Action<MovementStates> OnPlayerMove;
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float lighterFuel = 100.0f;

	[SerializeField] private bool lighterOn = true;

	[SerializeField] private GameObject lighter;
	public float WalkSpeed { get { return walkSpeed; } }
    public float RunSpeed { get { return runSpeed; } }
    public float SlowSpeed { get { return walkSpeed / 2.0f; } }
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

        Speed = WalkSpeed;
    }

    private void Start()
    {
        SetState(AliveState);
    }

    public void OnRun(InputValue value)
    {
        if(value.isPressed)
            Speed = RunSpeed;
        else
            Speed = WalkSpeed;
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

        if(Speed == RunSpeed)
        {
            OnPlayerMove?.Invoke(MovementStates.running);
        }
        else if(Speed == WalkSpeed)
        {
            OnPlayerMove?.Invoke(MovementStates.walking);
        }
        else
        {
            OnPlayerMove?.Invoke(MovementStates.slow);
        }

	}
	public void UseLighter()
    {
        lighter.SetActive(true);
        lighterOn = true;
    }

	public void HideLighter()
	{
		lighter.SetActive(false);
		lighterOn = false;

	}
    public void FuelBurner()
    {
        if (lighterOn && lighterFuel > 0)
        {
			lighterFuel -= 5 * Time.fixedDeltaTime;
		}
        else if (lighterFuel <= 0)
        {
			Debug.Log("lighter is false");

			lighter.SetActive(false);
        }
		Debug.Log(lighterFuel);
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
        controller.FuelBurner();
		//Debug.Log(lighter)

	}
}

public class PlayerDeadState : PlayerState
{
    public override void OnStart(PlayerController controller)
    {
        controller.SelfDestruct();
    }
}


