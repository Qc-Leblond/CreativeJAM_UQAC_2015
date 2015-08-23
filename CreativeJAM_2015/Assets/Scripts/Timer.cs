using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
    private bool TimerActive = false;
    public float TimeLeft { get; private set; }
    private float TimeStart = 300;
    private bool stopped;

	void Update () 
    {
	    if(TimerActive)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0) {
                TimerActive = false;
                GameManager.instance.OnGameEnd(GameManager.GameResult.lost);
            }
        }
	}

    public void TimerStart() //Fonction à appeller pour démarrer le timer.
    {
        TimeLeft = TimeStart;
        TimerActive = true;
    }
    public void TimerStop() {
        TimerActive = false;
    }
}
