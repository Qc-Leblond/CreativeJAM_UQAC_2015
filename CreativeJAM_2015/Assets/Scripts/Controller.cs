using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
    public bool isAxisInUse;         //Capte si le Dpad est utilisé
    public float MaxCam = 0.2f,      //Angle maximal de la caméra
                 MinCam = -0.2f,     //Angle minimal de la caméra
                 MoveSpeed = 5f,     //Vitesse de translation
                 TurnSpeed = 100f;   //Vitesse de rotation
    private int NbrObjet = 6,        //Nombre d'objets (VALEUR TEMPORAIRE)
                objectSelected = 0;  //Objet sélectioné 
    public GameObject Camera;        //Caméra attachée au joueur
    private Player player;           //Script vers Player

    void Awake()
    {
        player = this.GetComponent<Player>();
        isAxisInUse = false;
    }

	void Update() 
    {
        //Déplacements du personnage
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hCam = Input.GetAxis("HorizontalCamera");
        float vCam = Input.GetAxis("VerticalCamera");

        transform.Translate(-Vector3.forward * MoveSpeed * v * Time.deltaTime); //avant-arrière
        transform.Translate(Vector3.right * MoveSpeed * h * Time.deltaTime);    //gauche-droite
        transform.Rotate(Vector3.up * TurnSpeed * hCam * Time.deltaTime);       //caméra gauche-droite
        if (Camera.transform.rotation.x < MaxCam && vCam > 0 || Camera.transform.rotation.x > MinCam && vCam < 0)
            Camera.transform.Rotate(Vector3.right * TurnSpeed * vCam * Time.deltaTime);   ///caméra haut-bas

        //marche coquine lorsque B est appuyé
        if (Input.GetButtonDown("GirlWalk"))
        {
            player.isGirlWalking = true;
            MoveSpeed = 3f;
            Debug.Log("3");
        }

        //fin de la marche coquine lorsque B est relâché
        if (Input.GetButtonUp("GirlWalk"))
        {
            player.isGirlWalking = false;
            MoveSpeed = 5f;
            Debug.Log("5");
        }

        //Sélection des objets
        if (Input.GetAxisRaw("SelectUp") > 0 && objectSelected < NbrObjet && !isAxisInUse)
        {
            isAxisInUse = true;
            objectSelected++;
            Debug.Log(objectSelected);
        }

        if (Input.GetAxisRaw("SelectDown") < 0 && objectSelected > 0 && !isAxisInUse)
        {
            isAxisInUse = true;
            objectSelected--;
            Debug.Log(objectSelected);
        }

        if (Input.GetAxisRaw("SelectDown") == 0)
            isAxisInUse = false;
	}
}