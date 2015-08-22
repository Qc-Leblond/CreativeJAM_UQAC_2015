﻿using UnityEngine;
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
        Debug.Log("Test");
        if (other.tag == "Player") {
            Debug.Log("Test");
            RaycastHit hit;
            Physics.Raycast(girl.position, player.position - girl.position, out hit);
            if (hit.rigidbody.transform.root.tag == "Player") {
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
        Debug.Log("Test");
        if (other.tag == "Player" && doubtful) {
            girlAI.GoBackToPreviousState();
            doubtful = false;
        }
    }

}
