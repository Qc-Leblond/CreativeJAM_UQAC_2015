using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
    private bool isAxisInUse;         //Capte si le Dpad est utilisé
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
        Cursor.visible = false; 
        player = this.GetComponent<Player>();
        isAxisInUse = false;
    }

    void FixedUpdate()
    {
        float vCam = Input.GetAxis("VerticalCamera");
        if (Camera.transform.rotation.x < MaxCam && vCam > 0 || Camera.transform.rotation.x > MinCam && vCam < 0)
            Camera.transform.Rotate(Vector3.right * TurnSpeed * vCam * Time.deltaTime);   ///caméra haut-bas
    }
	void Update() 
    {
        //Déplacements du personnage
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hCam = Input.GetAxis("HorizontalCamera");
        //float vCam = Input.GetAxis("VerticalCamera");

        transform.Translate(-Vector3.forward * MoveSpeed * v * Time.deltaTime); //avant-arrière
        transform.Translate(Vector3.right * MoveSpeed * h * Time.deltaTime);    //gauche-droite
        transform.Rotate(Vector3.up * TurnSpeed * hCam * Time.deltaTime);       //caméra gauche-droite
        

        //marche coquine lorsque B est appuyé
        if (Input.GetButtonDown("GirlWalk"))
        {
            player.isGirlWalking = true;
            MoveSpeed = 2f;
        }

        //fin de la marche coquine lorsque B est relâché
        if (Input.GetButtonUp("GirlWalk"))
        {
            player.isGirlWalking = false;
            MoveSpeed = 5f;
        }

        //Sélection des objets par les triggers
        if (Input.GetAxisRaw("SelectTrigger") == -1 && objectSelected < NbrObjet && !isAxisInUse)
        {
            isAxisInUse = true;
            objectSelected++;
        }

        if (Input.GetAxisRaw("SelectTrigger") == 1 && objectSelected > 0 && !isAxisInUse)
        {
            isAxisInUse = true;
            objectSelected--;
        }

        if (Input.GetAxis("SelectTrigger") < 1 && Input.GetAxis("SelectTrigger") > -1)
            isAxisInUse = false;

        //Sélection des objects par Q et E
        if (Input.GetKeyDown(KeyCode.Q) && objectSelected > 0)
            objectSelected--;

        if (Input.GetKeyDown(KeyCode.E) && objectSelected < NbrObjet)
            objectSelected++;

        //Input pour crafter un item
        if (Input.GetButtonDown("CraftItem"))
        {
            //Do something fun :D
        }

        //Input pour utiliser un item
        if (Input.GetButtonDown("UseItem"))
        {
            //Do something fun :D
        }
	}
}