using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject [] pieceArray;
    public GameObject Filler;
    public GameObject Garage;
	public Color[] colorAray;
    public GameObject ressource;
	public int maxSizeX;
	public int maxSizeY;
	public int maxRooms;
	public float withRoom;
    public int maxRessource;
    public List<GameObject> listSpawnnerTemp;

	private int[,] corArray;
    private Color[,] colorRooms;
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
    public Vector3 garagePosition;

	// Use this for initialization
	public void SpawnGen() {
		corArray = new int[maxSizeX,maxSizeY];
        colorRooms = new Color[maxSizeX, maxSizeY];
		startPosition = Random.Range (0, maxSizeX);
		randomRoom = Random.Range (0, pieceArray.Length);
		instancePosition = new Vector3 (withRoom * startPosition, 0f, 0f);
        garagePosition = new Vector3(instancePosition.x, instancePosition.y, instancePosition.z - withRoom);

        GameObject maPiece = Instantiate(pieceArray[randomRoom], instancePosition, Quaternion.identity) as GameObject;
        setColorRoom(maPiece);
        GameObject monGarage = Instantiate(Garage, garagePosition, Quaternion.identity) as GameObject;

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

			if (instancePosition.z < ((maxSizeY - 1) * withRoom))
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

			if (instancePosition.x < ((maxSizeX - 1) * withRoom))
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
                randomIndexList = Random.Range(0, directionDispo.Count);
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

                maPiece = Instantiate(pieceArray[randomRoom], instancePosition, Quaternion.identity) as GameObject;

                setColorRoom(maPiece);
                //set la color
                directionDispo.Clear();
            }
			
		}

        spawns = GameObject.FindGameObjectsWithTag("spwannerRessource");
        List<GameObject> listSpawnner = new List<GameObject>(spawns);
        listSpawnnerTemp = listSpawnner;

        for (int i = 0; i < maxRessource; i++)
        {
            spawnUneRessource();
        }

        GameObject monFiller;
        for (int j = 0; j < maxSizeX; j++)
        {
            for (int k = 0; k < maxSizeY; k++)
            {

                if (corArray[j, k] < 1)
                {
                    fillerPosition.x = (withRoom * j);
                    fillerPosition.z = (withRoom * k);
                    
                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, (int)(fillerPosition.x / withRoom), (int)(fillerPosition.z / withRoom));
                }
           
                if (k == 0)
                {
                    fillerPosition.x = (withRoom * j);
                    fillerPosition.z = 0 - withRoom;
                    if (fillerPosition.x != garagePosition.x || fillerPosition.z != garagePosition.z)
                    {
                        monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                        setColorFiller(monFiller, (int)(fillerPosition.x / withRoom), (int)(fillerPosition.z / withRoom));
                    }
                }

                if (k == maxSizeY - 1)
                {
                    fillerPosition.x = (withRoom * j);
                    fillerPosition.z = maxSizeY * withRoom;

                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, (int)(fillerPosition.x / withRoom), (int)(fillerPosition.z / withRoom)); ;
                }

                if (j == 0)
                {
                    fillerPosition.x = 0 - withRoom;
                    fillerPosition.z = withRoom * k;

                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, (int)(fillerPosition.x / withRoom), (int)(fillerPosition.z / withRoom));
                }

                if (j == maxSizeX - 1)
                {
                    fillerPosition.x = maxSizeX * withRoom;
                    fillerPosition.z = k * withRoom;

                    monFiller = Instantiate(Filler, fillerPosition, Quaternion.identity) as GameObject;

                    setColorFiller(monFiller, (int)(fillerPosition.x / withRoom), (int)(fillerPosition.z / withRoom));
                }
            }
        }
	}

    void setColorRoom(GameObject maPiece)
    {
        int randomNumberColor = Random.Range(0, 5);
        foreach (Transform child in maPiece.transform)
        {
            if (child.CompareTag("Wall"))
            {
                child.GetComponent<MeshRenderer>().material.color = colorAray[randomNumberColor];
            }
        }

        randomRoom = Random.Range(0, pieceArray.Length);
        int colorRandom = Random.Range(0, pieceArray.Length);
        corArray[(int)(instancePosition.x / withRoom), (int)(instancePosition.z / withRoom)] = 1;
        colorRooms[(int)(instancePosition.x / withRoom), (int)(instancePosition.z / withRoom)] = colorAray[randomNumberColor];
    }

    void setColorFiller(GameObject monFiller, int x, int y)
    {
        foreach (Transform child in monFiller.transform)
        {
            if (child.CompareTag("WallR"))
            {
                if (x < maxSizeX - 1 && y <= maxSizeY - 1 && y >= 0)
                {
                    
                    child.GetComponent<MeshRenderer>().material.color = colorRooms[x + 1, y ];
                }
            }

            if (child.CompareTag("WallL"))
            {
                Debug.Log(x);
                Debug.Log(y);
                if (x > 0 && y >= 0 && y <= maxSizeY - 1)
                {
                    child.GetComponent<MeshRenderer>().material.color = colorRooms[x - 1, y];
                }
            }

            if (child.CompareTag("WallU"))
            {
                if (y < maxSizeY - 1 && x <= maxSizeX - 1 && x >= 0)
                {
                    child.GetComponent<MeshRenderer>().material.color = colorRooms[x, y + 1];
                }
            }

            if (child.CompareTag("WallD"))
            {
                if (y > 0 && x >= 0 && x <= maxSizeX - 1)
                {
                    child.GetComponent<MeshRenderer>().material.color = colorRooms[x, y - 1];
                }
            }
        } 
    }

    public void spawnUneRessource()
    {
        randomIndexList = Random.Range(0, listSpawnnerTemp.Count);
        Instantiate(ressource, listSpawnnerTemp[randomIndexList].transform.position, Quaternion.identity);
        listSpawnnerTemp.RemoveAt(randomIndexList);
    }
}
