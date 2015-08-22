using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject [] pieceArray;
	public GameObject Filler;
	public int maxSizeX;
	public int maxSizeY;
	public int maxRooms;
	public float withRoom;

	private int[,] corArray;
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

				Instantiate (pieceArray [randomRoom], instancePosition, Quaternion.identity);
				corArray [(int)(instancePosition.x / withRoom), (int)(instancePosition.z / withRoom)] = 1;
				directionDispo.Clear();
			}

            
       
		}

        for (int j = 0; j < maxSizeX; j++)
        {
            for (int k = 0; k < maxSizeY; k++)
            {
                if (corArray[j, k] < 1)
                {
                    fillerPosition.x = (withRoom * j);
                    fillerPosition.z = (withRoom * k);
                    Instantiate(Filler, fillerPosition, Quaternion.identity);

                    fillerPosition.x = -20;
                    fillerPosition.z = (withRoom * j);

                    Instantiate(Filler, fillerPosition, Quaternion.identity);

                    fillerPosition.x = withRoom * maxSizeY;
                    fillerPosition.z = (withRoom * j);

                    Instantiate(Filler, fillerPosition, Quaternion.identity);
                }
            }

            fillerPosition.x = (withRoom * j);
            fillerPosition.z = -20;

            Instantiate(Filler, fillerPosition, Quaternion.identity);
            
            fillerPosition.x = (withRoom * j);
            fillerPosition.z = withRoom * maxSizeY;

            Instantiate(Filler, fillerPosition, Quaternion.identity);
        }
	}
}
