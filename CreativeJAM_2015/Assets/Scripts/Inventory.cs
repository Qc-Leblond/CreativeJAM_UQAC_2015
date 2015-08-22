using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //BOOL JUSTE POUR TESTES UNE FONCTION
    public bool tester = false;


    public int nbrItem1;
    public int nbrItem2;
    public int nbrItem3;
    public int nbrItem4;

    public int indexObjectSelected;

    public GameObject[] inventory = new GameObject[4];
    public GameObject[] btnA = new GameObject[4];

	// Use this for initialization
	void Start () {
	    
        //Initialisation de variables
        nbrItem1 = 0;
        nbrItem2 = 0;
        nbrItem3 = 0;
        nbrItem4 = 0;

        indexObjectSelected = 0;
        //inventory[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
        changeColorAlphaAndBtnA();

        Cursor.visible = false;

	}

    void Update()
    {
        if (tester == true)
        {
            tester = false;
            changeInventoryIndex(1);
        }
    }

    void changeInventoryIndex(int value)
    {
        indexObjectSelected = indexObjectSelected + value;
        
        if(indexObjectSelected < 0)
        {
            indexObjectSelected = 3;
        }
        else
        {
            if(indexObjectSelected > 3)
            {
                indexObjectSelected = 0;
            }
        }

        changeColorAlphaAndBtnA();
        
    }

    void changeColorAlphaAndBtnA()
    {
        for (int x = 0; x < 4; x++)
        {
            if (x == indexObjectSelected)
            {
                //Debug.Log(x);
                inventory[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
                //Debug.Log(inventory[indexObjectSelected].GetComponent<Image>().color);
                btnA[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
                
            }
            else
            {
                //Debug.Log("Else");
                inventory[x].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                //Debug.Log(inventory[x].GetComponent<Image>().color);
                btnA[x].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }


}
