using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private Room [,] grid;

    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
        if (GameManager.instance != null)
        {
            GameManager.instance.mapGenerator = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMap()
    {

    }

    public void GenerateMap()
    {
        grid = new Room[cols, rows];

        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                tempRoomObj.transform.parent = this.transform;

                tempRoomObj.name = "room_" + currentCol + "," + currentRow;

                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //door managing
                //north/south
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else 
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                //east/west
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else 
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                
                grid [currentCol, currentRow] = tempRoom;
            }
        }
    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }
}
