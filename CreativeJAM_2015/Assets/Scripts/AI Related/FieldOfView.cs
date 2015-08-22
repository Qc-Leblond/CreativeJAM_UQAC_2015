using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

    Girl_AI girlAI;

    void Awake() {
        girlAI = GetComponentInParent<Girl_AI>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            girlAI.SwitchState(Girl_AI.State.doubtful);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            girlAI.GoBackToPreviousState();
        }
    }
}
