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
    //********************************************************************************************//
  

	// Use this for initialization
	void Awake () {

    }

    void update()
    {

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
}
