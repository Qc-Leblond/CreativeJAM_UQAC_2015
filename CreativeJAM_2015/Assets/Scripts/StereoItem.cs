using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StereoItem : MonoBehaviour {

    public GUIText textBait;
    public float duration;

    private List<GameObject> listFilles = new List<GameObject>(); 
    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        duration = duration - Time.deltaTime;
        textBait.text = "" + Mathf.Floor(duration);
        if(duration <= 0)
        {
            Destroy(transform.root.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Girl" && !listFilles.Contains(other.gameObject))
        {
            listFilles.Add(other.gameObject);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
            {
                if (hit.transform.tag == "Girl")
                {
                   hit.transform.GetComponent<Girl_AI>().SwitchState(Girl_AI.State.baited, duration, transform.position);
                }
            }
        }
    }
}
