using UnityEngine;
using System.Collections;

public class AI_DestinationPoint : MonoBehaviour {
    [HideInInspector]
    public Vector3 position;
    public bool isActive = true;

    void Awake() {
        position = new Vector3(transform.position.x, 5, transform.position.z);
    }
}
