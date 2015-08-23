using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour 
{
    public int Junk = 4;
    public int[] NbrItems = new int[4];
    public GameObject[] Nombres = new GameObject[4];

	void Update () 
    {
	    for(int i = 0; i < 3; i++)
        {
            Nombres[i].GetComponent<Text>().text = NbrItems[i].ToString();
        }
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
        NbrItems[Number]--;
    }
}
