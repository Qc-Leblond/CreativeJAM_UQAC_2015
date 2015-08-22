using UnityEngine;
using System.Collections;

public class GirlCharacter : MonoBehaviour {

    Girl_AI AI;

    void Start() {
        AI = GetComponent<Girl_AI>();
        Spawn();
    }

    void Spawn() {
        AI.currentPos = Girl_AI.possibleDestinations.GetRandomPoint(null);
        transform.position = AI.currentPos.position;
    }
}
