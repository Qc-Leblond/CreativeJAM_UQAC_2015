using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<Girl_AI>().SwitchState(Girl_AI.State.crying);
        Destroy(gameObject);
    }
}
