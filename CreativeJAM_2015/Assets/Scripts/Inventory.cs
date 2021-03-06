﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
    public int nbrItem1;
    public int nbrItem2;
    public int nbrItem3;
    public int nbrItem4;

    public int indexObjectSelected;

    public GameObject[] inventory = new GameObject[4];
    public GameObject[] btnA = new GameObject[4];
    public GameObject[] Use = new GameObject[4];
    public GameObject[] panel = new GameObject[4];

    public GameObject[] craftTable = new GameObject[4];
    public GameObject[] btnX = new GameObject[4];
    public GameObject[] Craft = new GameObject[4];
    public GameObject[] panelCraft = new GameObject[4];

    public GameObject CraftingActive;

	// Use this for initialization
	void Start () {
	    
        //Initialisation de variables
        nbrItem1 = 0;
        nbrItem2 = 0;
        nbrItem3 = 0;
        nbrItem4 = 0;

        indexObjectSelected = 0;
        //inventory[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
        changeColorAlphaAndBtn(panel, btnA, Use, inventory);

        Cursor.visible = false;

	}

    void Update()
    {
 
    }

    public void changeInventoryIndex(int value)
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

        changeColorAlphaAndBtn(panel, btnA, Use, inventory);
        
        if(CraftingActive.gameObject.activeSelf)
        {
            changeColorAlphaAndBtn(panelCraft, btnX, Craft, craftTable);

        }
        
    }

    void changeColorAlphaAndBtn(GameObject[] lesPanels,GameObject[] lesBtn,GameObject[] lesString, GameObject[] lesInventaire)
    {
        for (int x = 0; x < 4; x++)
        {
            if (x == indexObjectSelected)
            {
                //Debug.Log(x);
                //inventory[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
                lesPanels[indexObjectSelected].GetComponentInChildren<Image>().color = new Color(255, 255, 255);
                //Debug.Log(inventory[indexObjectSelected].GetComponent<Image>().color);
                lesBtn[indexObjectSelected].GetComponent<Image>().color = new Color(255, 255, 255);
                //btnA[indexObjectSelected].GetComponentInChildren<Image>().color = new Color(255, 255, 255);
                lesString[indexObjectSelected].GetComponent<Text>().color = new Color(0, 0, 0);
                lesInventaire[indexObjectSelected].GetComponentInChildren<Image>().color = new Color(255, 255, 255);

            }
            else
            {
                //Debug.Log("Else");
                //inventory[x].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                //Debug.Log(inventory[x].GetComponent<Image>().color);
                lesBtn[x].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                lesInventaire[x].GetComponentInChildren<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                //btnA[x].GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
                lesString[x].GetComponent<Text>().color = new Color(0, 0, 0, 0);
                lesPanels[x].GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);

            }
        }
    }
}
