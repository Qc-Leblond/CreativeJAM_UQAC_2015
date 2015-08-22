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

        [Header("Level Related")]
        public Scene currentScene = Scene.main;
        public MapGenerator mapGenerator;

    //********************************************************************************************//

    //Dans le Awake on gere la creation du singleton GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<Player>();
        switch (currentScene) {
            case Scene.menu:
                break;
            case Scene.main:
                OnMainLoad();
                StartCinematic();
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
                StartCinematic();
                break;
        }
    }

    #endregion

    #region Timer Related



    #endregion

    #region Intro Cinematic

    [Header("Intro Cinematic")]
    public Camera cinematicCamera;
    public GameObject cinematicCharacter;
    public float DelayBetween;
    public float finalHeight;
    public float risingSpeed;
    public float cinematicCameraAnimTime;
    private float cameraRotation;

    private void StartCinematic() {
        cinematicCamera.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        cinematicCamera.transform.localPosition = new Vector3(0, -0.2f, -1.6f);
        cinematicCharacter.transform.position = new Vector3(0, -1, 0.35f);
        cameraRotation = 0;
        StartCoroutine(CinematicAnim());
    }

    IEnumerator CinematicAnim() {
        Camera main = Camera.main;
        Camera.main.enabled = false;
        cinematicCamera.enabled = true;
        while (cinematicCharacter.transform.position.y < finalHeight) {
            cinematicCharacter.transform.position += new Vector3(0, risingSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(DelayBetween);
        while (!TurnCamera() & !MoveCamera()) {
            yield return new WaitForEndOfFrame();
        }
        cinematicCharacter.transform.position = new Vector3(-1000, -1000, -1000);
        yield return new WaitForSeconds(0.5f);
        main.enabled = true;
        cinematicCamera.enabled = false;
    }

    bool TurnCamera() {
        cinematicCamera.transform.Rotate(new Vector3(0, 180 / cinematicCameraAnimTime * Time.deltaTime, 0));
        return cameraRotation >= 180;
    }

    bool MoveCamera() {
        cinematicCamera.transform.localPosition += new Vector3(0, 0, 1.4f / cinematicCameraAnimTime * Time.deltaTime);
        return cinematicCamera.transform.localPosition.z >= -0.2f;
    }
    
    #endregion

}
