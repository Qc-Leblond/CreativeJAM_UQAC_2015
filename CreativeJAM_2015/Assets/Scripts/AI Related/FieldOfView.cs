using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

    Girl_AI girlAI;
    Transform player;
    Transform girl;
    bool doubtful;

    void Awake() {
        girlAI = GetComponentInParent<Girl_AI>();
        girl = girlAI.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doubtful = false;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            RaycastHit hit;
            Vector3 girlPos = girlAI.GetGirlPos();
            Physics.Raycast(girlPos, player.position -girlPos, out hit);
            if (hit.transform.root.tag == "Player") {
                girlAI.SwitchState(Girl_AI.State.doubtful);
                doubtful = true;
            }
            else {
                girlAI.GoBackToPreviousState();
                doubtful = false;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && doubtful) {
            girlAI.GoBackToPreviousState();
            doubtful = false;
        }
    }

}
