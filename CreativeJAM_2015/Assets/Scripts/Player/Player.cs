using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Declaration de variable
    //********************************************************************************************//
        
        //Variable qui represente les points "ego" du personnage. Une gauge representera sa valeur pour le joueur
        public float ego;

        //Variable qui sert a savoir si le joueur est entrain de marcher comme un fille (Cette variable sera modifier directement par le controler avec une fonction du genre SET)
        public bool isGirlWalking;

        //Variable qui garde en memoire le nombre de Junk que le joueur a amasser.
        public int nbrJunk;

        //Variable pour set le l'ego qu'on perd quand on girlWalk (on gagane de l'ego si on fait un tour)
        public float egoDownSpeed;  

        public UIManager uiManager;
    //********************************************************************************************//
  

	// Use this for initialization
	void Awake () {

        //Initialisation des variables
        isGirlWalking = false;
        nbrJunk = 0;  //Valeur temporaire
        ego = 100f; //Valeur temporaire

    }

    void Update()
    {
        if (isGirlWalking)
        {
            //On doit descendre l'ego du joueur
            ego = ego - egoDownSpeed;//Valeur set dans l'interface unity
            //Debug.Log(ego);
            uiManager.setEgoSliderValue(ego);
            //On verifie si l'ego est = 0
            if (ego <= 0)
            {
                setEgo(ego);
                Debug.Log("TU ES UNE FILLE !!!");
                //ICI METTRE LA MODIFICATION DANS L'INTERFACE
            }

        }
    }

    //Fonction qui retourne si le personnage est entrain de marcher comme une fille
    public bool returnIsGirlWalking()
    {
        return isGirlWalking;
    }

    //Fonction qui retourne le nbr de Junk que le personnage a amasser (nbrJunk)
    public int returnNbrJunk()
    {
        return nbrJunk;
    }

    //Fonction qui "set" le nbr de Junk (nbrJunk) quand le joueur en amasse un tas (recois en paramtre le nbr de junk a ajouter)
    public void setNbrJunk(int gainJunk)
    {
        nbrJunk = nbrJunk + gainJunk;
    }
    
    //Appeler dans le update pour le descendre et apres un prank par un autre script
    public void setEgo(float value)
    {
        uiManager.setEgoSliderValue(value);
    }
}
