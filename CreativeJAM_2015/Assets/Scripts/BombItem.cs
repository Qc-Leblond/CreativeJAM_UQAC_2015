﻿using UnityEngine;
using System.Collections;

public class BombItem : MonoBehaviour {

    public GUIText textBomb;
    public float radius;
	// Use this for initialization
	void Start () {
        StartCoroutine("explode");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator explode()
    {
        yield return new WaitForSeconds(1f);
        textBomb.text = "2";
        yield return new WaitForSeconds(1f);
        textBomb.text = "1";
        yield return new WaitForSeconds(1f);
        textBomb.text = "";
        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, hitColliders[i].transform.position - transform.position, out hit))
            {
                if (hit.transform.tag == "Girl")
                {
                    hit.transform.GetComponent<Girl_AI>().SwitchState(Girl_AI.State.crying);
                }
            }
            i++;
        }
        Destroy(gameObject.transform.root.gameObject);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Girl")
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
            {
                if(hit.transform.tag == "Girl")
                {
                    hit.transform.GetComponent<Girl_AI>().SwitchState(Girl_AI.State.crying);
                }
            }
        }
    }*/
}
