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
        doubtful
    }

    AIState previousState;
    AIState currentState;

    public bool isCrying = false;
    public bool isStuck = false;

    public static AI_DestinationPointsList possibleDestinations;
    [HideInInspector]
    public AI_DestinationPoint currentPos;

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
        SwitchState(State.idle);
    }

    void Update() {
        if (!isCrying) {
            currentState.Running();
        }
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void SwitchState(State state) {
        if (currentState != null) {
            currentState.Finish();
            previousState = currentState;
        }
        else {
            previousState = stateDictionary[State.idle];
        }
        currentState = stateDictionary[state];
        currentState.Execute();
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
    Transform girlPos;
    public override void Execute() {
        destination = Girl_AI.possibleDestinations.GetRandomPoint(girlAI.currentPos);
        girlAI.currentPos = destination;
        girlAI.girlPathingAI.destination = destination.position;
        if (girlPos == null) girlPos = girlAI.gameObject.transform;
    }

    public override void Running() {
        if (girlPos.position == destination.position) {
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
        timeBeforeMove = Random.Range(5f, 60f); //In secondes
    }
    public override void Running() {
        timeBeforeMove -= Time.deltaTime;
        if (timeBeforeMove < 0) {
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
    public override void Running() {
        //TODO Add to doubt meter in GameManager
    }
}

#endregion