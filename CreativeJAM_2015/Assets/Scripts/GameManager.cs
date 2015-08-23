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
        public float doubt {get; private set;}
        public float maxDoubt;

        [Header("Level Related")]
        public Scene currentScene = Scene.main;
        public MapGenerator mapGenerator;
        public Timer timer;
        public int numberOfGirlSpawned;
        [HideInInspector]
        public int currentNumberOfGirl;
        public GameObject characterController;

    //********************************************************************************************//

    //Dans le Awake on gere la creation du singleton GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            timer = gameObject.AddComponent<Timer>();
            switch (currentScene) {
                case Scene.menu:
                    break;

                case Scene.cinematicIntro:
                    StartCinematic();
                    break;

                case Scene.main:
                    mapGenerator.SpawnGen();
                    OnMainLoad();
                    break;
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }

    void Update() {
        if (currentScene == Scene.main) {
            if (doubt >= maxDoubt) {
                Debug.Log("Lost");
                OnGameEnd(GameResult.lost);
            }
            if (currentNumberOfGirl <= 0) {
                OnGameEnd(GameResult.won);
            }
            HandleGirlWalking();
        }
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

    public void AddDoubt(float mod) {
        if (mod > 0) {
            if (doubt <= maxDoubt)
                doubt += mod;
            else
                doubt = maxDoubt;
        }
        else {
            doubt -= mod;
            if (doubt < 0) doubt = 0;
        }
    }

    public void OnGirlCrying(){
        currentNumberOfGirl--;
        returnGirlRamaining();
    }

    public void returnGirlRamaining()
    {
        playerScript.uiManager.setGirlRamaining(currentNumberOfGirl);
    }

    #region On Level Load

    void OnMainLoad() {
        doubt = 0;
        OnGameStart();
    }

    #endregion

    #region Scene Handling

    public enum Scene {
        menu,
        cinematicIntro,
        main,
        recapEnd
    }

    public void SwitchScene(Scene scene) {
        currentScene = scene;
        switch (scene) {
            case Scene.menu:
                Application.LoadLevel("Intro");
                break;

            case Scene.cinematicIntro:
                StartCinematic();
                break;

            case Scene.main:
                Application.LoadLevel("Main");
                mapGenerator.SpawnGen();
                OnMainLoad();
                break;
            case Scene.recapEnd:
                Application.LoadLevel("Recap");
                break;
        }
    }

    #endregion

    #region Time Related

    public GameObject girl;

    public enum GameResult {
        won,
        lost
    }

    public void OnGameStart() {
        timer.TimerStart();
        playerGO = (Instantiate(characterController, mapGenerator.garagePosition, Quaternion.identity) as GameObject);//Spawn Player
        playerScript = playerGO.GetComponentInChildren<Player>();
        currentNumberOfGirl = numberOfGirlSpawned;
        //Spawn Girl
        for (int i = 0; i < numberOfGirlSpawned; i++) {
            Instantiate(girl);
        }
    }

    public void OnGameEnd(GameResult result) {
        timer.TimerStop();
        if (result == GameResult.won) AddToScore(Mathf.CeilToInt(timer.TimeLeft * 10), Vector3.zero, ScoreType.time, false);
        AddToScore(Mathf.CeilToInt(-doubt * 5), Vector3.zero, ScoreType.doubt, false);
        StartCoroutine(OnGameEndCoroutine());
    }

    IEnumerator OnGameEndCoroutine() {
        yield return new WaitForSeconds(1); //Temporary TODO
        SwitchScene(Scene.recapEnd);
    }

    #endregion

    #region Intro Cinematic

    [Header("Intro Cinematic")]
    public Camera cinematicCamera;
    public float DelayBetween;
    public float finalHeight;
    public float risingSpeed;
    public float cinematicCameraAnimTime;

    private void StartCinematic() {
        cinematicCamera.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StartCoroutine(CinematicAnim());
    }

    IEnumerator CinematicAnim() {
        GameObject.FindGameObjectWithTag("IntroAnim").GetComponent<Animator>().SetBool("Start", true);
        yield return new WaitForSeconds(DelayBetween);
        SwitchScene(Scene.main);
    }

    /*bool TurnCamera() {
        cinematicCamera.transform.Rotate(new Vector3(0, 180 / cinematicCameraAnimTime * Time.deltaTime, 0));
        return cameraRotation >= 180;
    }*/

    bool MoveCamera(float speed, float destination) {
        cinematicCamera.transform.localPosition += new Vector3(0, 0, speed / cinematicCameraAnimTime * Time.deltaTime);
        return cinematicCamera.transform.localPosition.z >= destination;
    }
    
    #endregion

    #region Score

    public int score { get; private set; }
    public GameObject scoreText;
    public int scoreDoubt = 0; //Implemented
    public int scoreTime = 0; //Implemented
    public int scoreCrying = 0; //Implemented
    public int scoreCombo = 0;

    public enum ScoreType {
        doubt,
        time,
        crying, 
        combo
    }


    public void AddToScore(int modification, Vector3 pos, ScoreType type, bool visual = true) {
        if (visual) {
            TextMesh display = ((GameObject)Instantiate(scoreText, pos + new Vector3(0, 5, 0), Quaternion.Euler(Vector3.zero))).GetComponent<TextMesh>();
            display.transform.localScale = new Vector3(Mathf.CeilToInt(modification / 300), Mathf.CeilToInt(modification / 300), 1);
            StartCoroutine(ScoreTextAnim(display.gameObject));
        }
        switch (type) {
            case ScoreType.doubt:
                scoreDoubt += modification;
                break;
            case ScoreType.time:
                scoreTime += modification;
                break;
            case ScoreType.crying:
                scoreCrying += modification;
                break;
            case ScoreType.combo:
                scoreCombo += modification;
                break;
        }
    }

    IEnumerator ScoreTextAnim(GameObject display) {
        Vector3 pos = display.transform.position;
        float time = 2;
        float alpha = 255f;
        while (display.transform.position.y < pos.y + 10) {
            MeshRenderer render = display.GetComponent<MeshRenderer>();
            alpha -= 255f / time * Time.deltaTime;
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, alpha/255);
            display.transform.position += new Vector3(0, 10 / time * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        Destroy(display);
    }

    #endregion

}
