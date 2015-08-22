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
       public float doubt
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

        //On garde en memoire le script du player pour y avoir acces 
        playerScript = playerGO.GetComponent<Player>();

        doubt = 0;
    }

    void update()
    {
        
    }	
}
