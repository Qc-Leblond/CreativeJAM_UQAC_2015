using UnityEngine;
using System.Collections;

public class Ressource : MonoBehaviour {

    public int speedRotation;

    bool goUp = true;
	// Use this for initialization
	void Start () {
	
	}
	 
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, speedRotation, 0) * Time.deltaTime);
        
        if(transform.position.y > 0.6f)
        {
            goUp = false;
        }
        else if(transform.position.y < 0.4f)
        {
            goUp = true;
        }

        if(goUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime / 4);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime / 4);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Girl")
        {
            if (GameManager.instance.mapGenerator.listSpawnnerTemp.Contains(gameObject))
            {
                GameManager.instance.mapGenerator.listSpawnnerTemp.Remove(gameObject);
            }
            GameManager.instance.mapGenerator.spawnUneRessource();
            GameManager.instance.mapGenerator.listSpawnnerTemp.Add(gameObject);
        }
    }
}
