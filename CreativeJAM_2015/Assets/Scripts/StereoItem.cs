using UnityEngine;
using System.Collections;

public class StereoItem : MonoBehaviour {

    public GUIText textBait;
    public float duration;
    // Use this for initialization
    void Awake()
    {
        StartCoroutine("play");
    }

    // Update is called once per frame
    void Update()
    {
        duration = duration - Time.deltaTime;
        textBait.text = "" + Mathf.Floor(duration);
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Girl")
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
            {
                if (hit.transform.tag == "Girl")
                {
                   // hit.transform.GetComponent<Girl_AI>().SwitchState(Girl_AI.State.);
                }
            }
        }
    }
}
