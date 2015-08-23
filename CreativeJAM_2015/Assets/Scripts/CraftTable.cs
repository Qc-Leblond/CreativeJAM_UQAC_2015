using UnityEngine;
using System.Collections;

public class CraftTable : MonoBehaviour {

    public GameObject craftingUI;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "craftingTable")
        {
            craftingUI.SetActive(true);
        }
    }
}
