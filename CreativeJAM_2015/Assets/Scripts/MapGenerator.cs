using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject [] pieceArray;
    public GameObject Filler;
	public Material[] colorAray;
    public GameObject ressource;
	public int maxSizeX;
	public int maxSizeY;
	public int maxRooms;
	public float withRoom;
    public int maxRessource;

	private int[,] corArray;
    private int[,] colorRooms;
    GameObject[] spawns;
	private List <char> directionDispo = new List<char>();
    
	int startPosition;
	int positionY;
	int positionX;
	int randomRoom;
	int randomIndexList;
	char caseSwitch;
	Vector3 instancePosition;
	Vector3 fillerPosition;

	// Use this for initialization
	void Awake () {
		corArray = new int[maxSizeX,maxSizeY];
        colorRooms = new int[maxSizeX, maxSizeY];
		startPosition = Random.Range (0, maxSizeX);
		randomRoom = Random.Range (0, pieceArray.Length);
		instancePosition = new Vector3 (withRoom * startPosition, 0f, 0f);
		fillerPosition = new Vector3 (instancePosition.x, instancePosition.y, instancePosition.z - withRoom);

		Instantiate (pieceArray [randomRoom], instancePosition, Quaternion.identity);
		Instantiate (Filler, fillerPosition, Quaternion.identity);

		corArray [startPosition, 0] = 1;

		for (int i = 0; i < maxRooms - 1; i++) 
		{
			if (instancePosition.z > 0)
			{
                if (corArray[((int)instancePosition.x) / (int)withRoom, ((int)instancePosition.z - (int)withRoom) / (int)withRoom] < 1)
				{
					directionDispo.Add('d');
				}
			}

			if (instancePosition.z < (maxSizeY - 1) * withRoom )
			{
                if (corArray[((int)instancePosition.x) / (int)withRoom, ((int)instancePosition.z + (int)withRoom) / (int)withRoom] < 1)
				{
					directionDispo.Add('u');
				}
			}

			if (instancePosition.x > 0)
			{
                if (corArray[((int)instancePosition.x - (int)withRoom) / (int)withRoom, ((int)instancePosition.z) / (int)withRoom] < 1)
				{
					directionDispo.Add('l');
				}
			}

			if (instancePosition.x < (maxSizeX - 1) * withRoom )
			{
                if (corArray[((int)instancePosition.x + (int)withRoom) / (int)withRoom, ((int)instancePosition.z) / (int)withRoom] < 1)
				{
					directionDispo.Add('r');
				}
			}

			if (directionDispo.Count < 1)
			{
				i = maxRooms;
			}
			else
			{
				randomIndexList = Random.Range (0, directionDispo.Count);
				caseSwitch = directionDispo[randomIndexList];

				switch (caseSwitch)
				{
				case 'u':
					instancePosition.z = instancePosition.z + withRoom;
					break;
				case 'r':
					instancePosition.x = instancePosition.x + withRoom;
					break;
				case 'd':
					instancePosition.z = instancePosition.z - withRoom;
					break;
				case 'l':
					instancePosition.x = instancePosition.x - withRoom;
					break;
				default:
					Debug.Log("Error");
					break;
				}

				GameObject maPiece = Instantiate (pieceArray [randomRoom], instancePosition, Quaternion.identity) as GameObject;
                int colorRandom = Random.Range(0, pieceArray.Length);
				corArray [(int)(instancePosition.x / withRoom), (int)(instancePosition.z / withRoom)] = 1;
                colorRooms[(int)(instancePosition.x / withRoom), (int)(instancePosition.z / withRoom)] = colorRandom;
                //set la color
				directionDispo.Clear();
			}
		}

        spawns = GameObject.FindGameObjectsWithTag("spwannerRessource");
        List<GameObject> listSpawnner = new List<GameObject>(spawns);
        List<GameObject> listSpawnnerTemp = listSpawnner;

        for (int i = 0; i < maxRessource; i++)
        {
            randomIndexList = Random.Range(0, listSpawnnerTemp.Count);
            Instantiate(ressource, listSpawnnerTemp[randomIndexList].transform.position, Quaternion.identity);
            listSpawnnerTemp.RemoveAt(randomIndexList);
        }

        for (int j = 0; j < maxSizeX; j++)
        {
            GameObject monFiller;
            for (int k = 0; k < maxSizeY; k++)
            {
                if (corArray[j, k] < 1)
                {
                    fillerPosition.x = (withRoom * j);
                    fillerPosition.z = (withRoom * k);
                    
                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, j, k);

                    fillerPosition.x = - withRoom;
                    fillerPosition.z = (withRoom * j);

                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, j, k);

                    fillerPosition.x = withRoom * maxSizeY;
                    fillerPosition.z = (withRoom * j);

                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, j, k);
                }
            }

            fillerPosition.x = (withRoom * j);
            fillerPosition.z = - withRoom;

            monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

            setColorFiller(monFiller, j, (int)fillerPosition.z);
            
            fillerPosition.x = (withRoom * j);
            fillerPosition.z = withRoom * maxSizeY;

            monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

            setColorFiller(monFiller, j, (int)(fillerPosition.z / withRoom));
        }
	}

    void setColorFiller(GameObject monFiller, int x, int y)
    {
        /*MeshRenderer maDoor;
        int maColor;
        
        if (x < maxSizeX - 1)
        {
            maDoor = monFiller.transform.FindChild("DoorE").GetComponent<MeshRenderer>();
            maColor = colorRooms[x, y];
            //add la texture
        }

        if (x > 0)
        {
            maDoor = monFiller.transform.FindChild("DoorO").GetComponent<MeshRenderer>();
            maColor = colorRooms[x, y];
            //add la texture
        }

        if (y > 0)
        {
            maDoor = monFiller.transform.FindChild("DoorS").GetComponent<MeshRenderer>();
            maColor = colorRooms[x, y];
            //add la texture
        }

        if (y < maxSizeY - 1)
        {
            maDoor = monFiller.transform.FindChild("DoorN").GetComponent<MeshRenderer>();
            maColor = colorRooms[x, y];
            //add la texture
        }*/
    }
}
