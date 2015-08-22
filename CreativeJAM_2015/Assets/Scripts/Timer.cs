using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
    private bool TimerActive = false;
    private float TimeLeft,
                  TimeStart = 30;

	void Update () 
    {
	    if(TimerActive)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0)
                TimerActive = false;
        }
	}

    void TimerStart()
    {
        TimeLeft = TimeStart;
        TimerActive = true;
    }
}
