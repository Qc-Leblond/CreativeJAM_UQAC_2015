using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour 
{
    public int Junk = 4;
    public int[] NbrItems = new int[4];
    public GameObject[] Nombres = new GameObject[5];

	void Update () 
    {
	    for(int i = 0; i < 4; i++)
        {
            Nombres[i].GetComponent<Text>().text = NbrItems[i].ToString();
        }

        Nombres[4].GetComponent<Text>().text = Junk.ToString();
	}

    public void AddItem(int Number, int prixJunk) 
    {
        if(prixJunk <= Junk)
        {
            NbrItems[Number]++;
            Junk -= prixJunk;
        } 
    }

    public void RemoveItem(int Number)
    {
        if(NbrItems[Number] > 0)
        {
            NbrItems[Number]--;
        }
    }   
}
