using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //Dans le GameManager on gere le singleton......

    //Declaration de variable
    //********************************************************************************************//
        public static GameManager instance;
        
        //Variable public qui contient le joueur (On le trouve directement dans l'interface de unity)
        public GameObject playerGO;

        //Varaible qui contient le scrpit du player
        private Player playerScript;

        //Variable qui contient le doute de sur le joueur (doubt)
        private float doubt;    

        //Variable pour le get et set le doubt
        public float getSetDoubt
        {
            get
            {
                return doubt;
            }
            private set
            {
                //Ici si jamais on veut gere quoi faire avant que le doubt se rendre soit <= 0
                Debug.Log("Valeur du doute :" + value);
                doubt = value;
            }
        }
        
    //********************************************************************************************//

    //Dans le Awake on gere la creation du singleton GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }

        playerScript = playerGO.GetComponent<Player>();

        initiatePlayersStates(playerScript);

        doubt = 0;
        
    }

    void update()
    {
        if (playerScript.returnIsGirlWalking())
        {
            //On doit descendre l'ego du joueur
            playerScript.ego = playerScript.ego - 0.5f;//Valeur temporaire

            //On verifie si l'ego est = 0
            if (playerScript.ego <= 0)
            {
                Debug.Log("TU ES UNE FILLE !!!");
            }
        }
    }

    //Fonction pour initialiser les info sur le joueur ego, isGirlWalking etc...
    void initiatePlayersStates(Player thePlayerScript)
    {
        thePlayerScript.nbrJunk = 0; //Valeur temporaire
        thePlayerScript.isGirlWalking = false;
        thePlayerScript.ego = 100f; //Valeur temporaire
    }


	
}
