using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //Dans le GameManager on gere le singleton......

    //Declaration de variable
    //********************************************************************************************//
        public static GameManager instance;
        
        //Variable public qui contient le joueur (On le trouve directement dans l'interface de unity)
        [HideInInspector]
        public GameObject playerGO;

        //Varaible qui contient le scrpit du player
        [HideInInspector]
        public Player playerScript;

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

        [Header("Level Related")]
        public Scene currentScene = Scene.main;
        public MapGenerator mapGenerator;

    //********************************************************************************************//

    //Dans le Awake on gere la creation du singleton GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = new GameManager();
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        playerScript = playerGO.GetComponent<Player>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<Player>();
        switch (currentScene) {
            case Scene.menu:
                break;
            case Scene.main:
                OnMainLoad();
                break;
        }
    }

    void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }

    void update() {
        HandleGirlWalking();
    }

    void HandleGirlWalking() {
        if (playerScript.returnIsGirlWalking()) {
            //On doit descendre l'ego du joueur
            playerScript.ego = playerScript.ego - 0.5f;//Valeur temporaire

            //On verifie si l'ego est = 0
            if (playerScript.ego <= 0) {
                Debug.Log("TU ES UNE FILLE !!!");
            }
        }
    }

    #region On Level Load

    void OnMainLoad() {
        initiatePlayersStates(playerScript);
        doubt = 0;
    }

    //Fonction pour initialiser les info sur le joueur ego, isGirlWalking etc...
    void initiatePlayersStates(Player thePlayerScript)
    {
        thePlayerScript.nbrJunk = 0; //Valeur temporaire
        thePlayerScript.isGirlWalking = false;
        thePlayerScript.ego = 100f; //Valeur temporaire
    }

    #endregion

    #region Scene Handling

    public enum Scene {
        menu,
        main,
    }

    public void SwitchScene(Scene scene) {
        switch (scene) {
            case Scene.menu:
                Application.LoadLevel("Menu");
                break;

            case Scene.main:
                Application.LoadLevel("Main");
                OnMainLoad();
                break;
        }
    }

    #endregion

    #region Timer Related



    #endregion

    #region Intro Cinematic

    [Header("Intro Cinematic")]
    public Camera cinematicCamera;
    public float DelayBetween;
    public float finalHeight;
    public float risingSpeed;
    public float cinematicCameraSpeed;
    public float cinematicCameraTurnSpeed;

    private void StartCinematic() {

        //StartCoroutine(CinematicAnim());
    }

    /*IEnumerator CinematicAnim() {
        while (playerGO.transform.position.y < finalHeight) {
            transform.localPosition += new Vector3(0, risingSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(DelayBetween);
        while (!TurnCamera() & !MoveCamera()) {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
    }

    bool TurnCamera() {
        cinematicCamera.transform.rotation = Quaternion.Euler(new Vector3(cinematicCamera.transform.rotation.x,
                                                                          cinematicCamera.transform.rotation.y + cinematicCameraTurnSpeed,
                                                                          cinematicCamera.transform.rotation.z));
    }

    bool MoveCamera() {

    }*/
    
    #endregion
}
