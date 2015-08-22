using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
    private bool TimerActive = false;
    private float TimeLeft,
                  TimeStart = 300;

	void Update () 
    {
	    if(TimerActive)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0)
                TimerActive = false;
        }
	}

    void TimerStart() //Fonction à appeller pour démarrer le timer.
    {
        TimeLeft = TimeStart;
        TimerActive = true;
    }
}