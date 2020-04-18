using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    #region Variables
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;
    public float waitTime = 10f;
    private bool spawnedBoss;
    public GameObject boss;

    #endregion

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            spawnedBoss = true;

        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
