using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Font FontName;
    void OnGUI()
    {
        GUIStyle StyleName = new GUIStyle();
        StyleName.font = FontName;
        StyleName.fontSize = 20;
        StyleName.normal.textColor = Color.white;

        if (transform.position.y == 0)
        {
            if (GUI.Button(new Rect(Screen.width / 2, 50, 200, 50), "Jouer", StyleName))
                Application.LoadLevel("SceneCostaud");
            if (GUI.Button(new Rect(Screen.width / 2, 100, 350, 50), "Contrôles", StyleName))
                transform.position = new Vector3(0, 8, -11);
            if (GUI.Button(new Rect(Screen.width / 2, 150, 350, 50), "Instructions", StyleName))
                transform.position = new Vector3(0, -8, -11);
            if (GUI.Button(new Rect(Screen.width / 2, 200, 260, 50), "Quitter", StyleName))
                Application.Quit();
        }
        else if (transform.position.y == 8)
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4, 350, 50), "Retour", StyleName))
                transform.position = new Vector3(0, 0, -11);
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4, 350, 50), "Retour", StyleName))
                transform.position = new Vector3(0, 0, -11);
        }
    }
}