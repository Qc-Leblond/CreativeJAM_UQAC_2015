using UnityEngine;
using System.Collections;

public class AI_DestinationPoint : MonoBehaviour {
    [HideInInspector]
    public Vector3 position;
    public bool isActive = true;

    void Awake() {
        position = transform.position;
    }
}
