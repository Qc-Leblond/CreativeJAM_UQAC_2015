﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider egoSlider;
    public Slider doubtSlider;

    public Text girlRemainingText;

    public GameObject panelVieEnRose;

	// Use this for initialization
	void Start () {

        egoSlider.value = 100;
        //doubtSlider.value = 0;

        setGirlRamaining(GameManager.instance.currentNumberOfGirl);
	}
	
	// Update is called once per frame
	void Update () {
	
        

	}

    public void setEgoSliderValue(float value)
    {
        egoSlider.value = value;
        //Debug.Log(value);
        panelVieEnRose.GetComponent<Image>().CrossFadeAlpha(100, 3f, false);
    }

    public void setGirlRamaining(int value)
    {
        girlRemainingText.text = value.ToString();
    }
}
