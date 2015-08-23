using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Girl_AI : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent girlPathingAI;
    Dictionary<State, AIState> stateDictionary = new Dictionary<State, AIState>();
    public enum State {
        moving,
        idle,
        crying,
        occupied,
        doubtful,
        baited
    }

    AIState previousState;
    AIState currentState;

    public State state;

    public bool isCrying = false;
    public bool isStuck = false;
    public bool isBaited = false;

    public static AI_DestinationPointsList possibleDestinations;
    [HideInInspector]
    public AI_DestinationPoint currentPos;
    [Header("Field of view")]
    public GameObject fieldOfView;
    public MeshRenderer fieldOfViewVisual;
    public Color noDoubt;
    public Color isDoubting;

    public float girlPosY;
    public float lookatRotationSpeed;

    public Vector3 GetGirlPos() {
        return new Vector3(transform.position.x, girlPosY, transform.position.z);
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Awake() {
        if (possibleDestinations == null)
            possibleDestinations = new AI_DestinationPointsList(GameObject.FindObjectsOfType<AI_DestinationPoint>());
        girlPathingAI = GetComponent<NavMeshAgent>();
        stateDictionary.Add(State.moving, new AIState_Moving(this));
        stateDictionary.Add(State.idle, new AIState_Idle(this));
        stateDictionary.Add(State.crying, new AIState_Crying(this));
        stateDictionary.Add(State.occupied, new AIState_Occupied(this));
        stateDictionary.Add(State.doubtful, new AIState_Doubtful(this));
        stateDictionary.Add(State.baited, new AIState_Baited(this));
        SwitchState(State.idle);
        fieldOfViewVisual.material.color = noDoubt;
    }

    void Start() {
        girlPathingAI.destination = currentPos.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
    }

    void Update() {
        if (!isCrying) {
            currentState.Running();
        }

        if (Input.GetKeyDown(KeyCode.T)) SwitchState(State.moving);
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void SwitchState(State newState) {
        if (!isCrying && !isBaited) {
            if (currentState != null) {
                currentState.Finish();
                previousState = currentState;
            }
            else {
                previousState = stateDictionary[State.idle];
            }
            state = newState;
            currentState = stateDictionary[newState];
            currentState.Execute();
            
        }
    }

    public void SwitchState(State newState, float time, Vector3 destination) {
        if (!isCrying && !isBaited) {
            if (currentState != null) {
                currentState.Finish();
                previousState = currentState;
            }
            else {
                previousState = stateDictionary[State.idle];
            }
            currentState = stateDictionary[newState];
            if (newState == State.baited) {
                ((AIState_Baited)currentState).baitedTime = time;
                ((AIState_Baited)currentState).destination = destination;
            }
            currentState.Execute();
            
        }
    }

    public void GoBackToPreviousState() {
        AIState temp = currentState;
        currentState = previousState;
        previousState = temp;
    }
    
}

#region States

#region Abstract State
public class AIState {
    protected Girl_AI girlAI;

    public AIState(Girl_AI ai) {
        girlAI = ai;
    }

    public virtual void Execute() {
        return;
    }
    public virtual void Running() {
        return;
    }
    public virtual void Finish() {
        return;
    }
}

#endregion

/// <summary>
/// AI State when Moving
/// </summary>
public class AIState_Moving : AIState {
    public AIState_Moving(Girl_AI ai) : base(ai) {  }
    AI_DestinationPoint destination;
    float desiredRotation;
    Transform girlPos;
    public override void Execute() {
        destination = Girl_AI.possibleDestinations.GetRandomPoint(girlAI.currentPos);
        girlAI.currentPos = destination;
        girlAI.girlPathingAI.destination = destination.position;
        desiredRotation = Random.Range(0f, 360f);
        if (girlPos == null) girlPos = girlAI.gameObject.transform;
    }

    public override void Running() {
        if (!girlAI.girlPathingAI.hasPath || girlAI.girlPathingAI.velocity.sqrMagnitude == 0f) {
            girlAI.transform.rotation = Quaternion.Slerp(girlAI.transform.rotation,
                                                         Quaternion.Euler(new Vector3(0, desiredRotation, 0)),
                                                         Time.deltaTime * girlAI.lookatRotationSpeed);
            if (girlAI.transform.rotation.y == desiredRotation)
                girlAI.SwitchState(Girl_AI.State.idle);
        }
    }
}

/// <summary>
/// AI Idle until otherwise
/// </summary>
public class AIState_Idle : AIState {
    float timeBeforeMove;
    public AIState_Idle(Girl_AI ai) : base(ai) { }
    public override void Execute() {
        timeBeforeMove = Random.Range(5f, 30f); //In secondes
    }
    public override void Running() {
        timeBeforeMove -= Time.deltaTime;
        if (timeBeforeMove <= 0) {
                girlAI.SwitchState(Girl_AI.State.moving);
        }
    }
}

/// <summary>
/// Basicaly dead AI
/// </summary>
public class AIState_Crying : AIState {
    public AIState_Crying(Girl_AI ai) : base(ai) {  }
    public override void Execute() {
        girlAI.isCrying = true;
    }
}

/// <summary>
/// State that you can only get out by force
/// </summary>
public class AIState_Occupied : AIState {
    public AIState_Occupied(Girl_AI ai) : base(ai) {  }
}


/// <summary>
/// State when field of view is on player and not using ability to go unnoticed
/// </summary>
public class AIState_Doubtful : AIState {
    public AIState_Doubtful(Girl_AI ai) : base(ai) {  }
    Transform playerPos;
    public override void Execute() {
        girlAI.fieldOfViewVisual.material.color = girlAI.isDoubting;
        girlAI.girlPathingAI.Stop();
        if (playerPos == null) {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public override void Running() {
        Debug.DrawLine(new Vector3(playerPos.position.x, girlAI.girlPosY, playerPos.position.z), girlAI.GetGirlPos(), Color.red, 1f);
        girlAI.transform.rotation = Quaternion.Slerp(girlAI.transform.rotation,
                                                     Quaternion.LookRotation(new Vector3(playerPos.position.x, girlAI.girlPosY, playerPos.position.z) - girlAI.GetGirlPos()),
                                                     Time.deltaTime * girlAI.lookatRotationSpeed);
    }
    public override void Finish() {
        girlAI.fieldOfViewVisual.material.color = girlAI.noDoubt;
        girlAI.girlPathingAI.Resume();
    }
}

/// <summary>
/// When the girl is baited by an object
/// </summary>
public class AIState_Baited : AIState {
    public AIState_Baited(Girl_AI ai) : base(ai) {  }
    public float baitedTime;
    public Vector3 destination;
    public override void Execute() {
        girlAI.fieldOfView.SetActive(false);
        float radius = Random.Range(4f, 11f);
        float angle = Random.Range(0f, 360f);
        girlAI.girlPathingAI.destination = new Vector3(destination.x + radius * Mathf.Cos(angle), destination.y, destination.z + radius * Mathf.Sin(angle));
    }
    public override void Running() {
        baitedTime -= Time.deltaTime;
        if (baitedTime < 0) {
            girlAI.SwitchState(Girl_AI.State.idle);
        }
    }
    public override void Finish() {
        girlAI.fieldOfView.SetActive(true);
        girlAI.isBaited = false;
    }
}

#endregion