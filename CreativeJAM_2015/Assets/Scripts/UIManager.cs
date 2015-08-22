using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider egoSlider;
    public Slider doubtSlider;

	// Use this for initialization
	void Start () {

        egoSlider.value = 100;
        //doubtSlider.value = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
        

	}

    public void setEgoSliderValue(float value)
    {
        egoSlider.value = value;
    }
}
