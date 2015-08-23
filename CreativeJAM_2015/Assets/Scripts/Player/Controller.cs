using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
    private bool isAxisInUse;        //Capte si le Dpad est utilisé
    public float MaxCam = 20f,       //Angle maximal de la caméra
                 MinCam = -20f,      //Angle minimal de la caméra
                 MoveSpeed = 5f,     //Vitesse de translation
                 TurnSpeed = 5f;     //Vitesse de rotation
    private float RotationX,         //Rotation du joueur sur l'axe X
                  RotationY = 0f;    //Rotation de la caméra sur l'axe Y
    private int NbrObjet = 3,        //Nombre d'objets (VALEUR TEMPORAIRE)
                objectSelected = 0;  //Objet sélectioné 
    public GameObject Camera;        //Caméra attachée au joueur
    private Player player;           //Script vers Player
    private Rigidbody rigidBody;
    private Inventaire inventaire;
    private Inventory inventory;
    
    void Awake()
    {
        Cursor.visible = false; 
        player = this.GetComponent<Player>();
        isAxisInUse = false;
        rigidBody = GetComponent<Rigidbody>();
        inventaire = GetComponent<Inventaire>();
        inventory = GetComponent<Inventory>();
    }

    void Update ()
    {
        //Déplacement du personnage et de la caméra
        RotationX = transform.localEulerAngles.y + Input.GetAxis("HorizontalCamera") * MoveSpeed;
        RotationY += Input.GetAxis("VerticalCamera") * TurnSpeed;
        RotationY = Mathf.Clamp (RotationY, MinCam, MaxCam);
        Camera.transform.localEulerAngles = new Vector3(-RotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, RotationX, 0);

        transform.Translate(-Vector3.forward * MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime); //personnage avant-arrière
        transform.Translate(Vector3.right * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);    //personnage gauche-droite

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
            inventory.changeInventoryIndex(1);
        }

        if (Input.GetAxisRaw("SelectTrigger") == 1 && objectSelected > 0 && !isAxisInUse)
        {
            isAxisInUse = true;
            objectSelected--;
            inventory.changeInventoryIndex(-1);
        }

        if (Input.GetAxis("SelectTrigger") < 1 && Input.GetAxis("SelectTrigger") > -1)
            isAxisInUse = false;

        //Sélection des objects par Q et E
        if (Input.GetKeyDown(KeyCode.Q) && objectSelected > 0)
        {
            objectSelected--;
            inventory.changeInventoryIndex(-1);
        }
            

        if (Input.GetKeyDown(KeyCode.E) && objectSelected < NbrObjet)
        {
            objectSelected++;
            inventory.changeInventoryIndex(1);
        }
            
        //Input pour crafter un item
        if (Input.GetButtonDown("CraftItem"))
        {
            switch (objectSelected)
            {
                case 0:
                    inventaire.AddItem(0, 4);
                    break;
                case 1:
                    inventaire.AddItem(1, 4);
                    break;
                case 2:
                    inventaire.AddItem(2, 4);
                    break;
                case 3:
                    inventaire.AddItem(3, 4);
                    break;
            }
        }

        //Input pour utiliser un item
        if (Input.GetButtonDown("UseItem"))
        {
            switch(objectSelected)
            {
                case 0:
                    inventaire.RemoveItem(0);
                    break;
                case 1:
                    inventaire.RemoveItem(1);
                    break;
                case 2:
                    inventaire.RemoveItem(2);
                    break;
                case 3:
                    inventaire.RemoveItem(3);
                    break;
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = false;
    }
}