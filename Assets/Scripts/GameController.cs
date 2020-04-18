using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameController : MonoBehaviour
{

    public RoomTemplates rt;
    public GameObject Looter;
    public GameObject treasure;
    public GameObject PathFinderController;


    // Start is called before the first frame update
    void Start()
    {
        rt = GameObject.Find("Entry Room").GetComponent<RoomTemplates>();
        Invoke("populateRooms", rt.waitTime);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void populateRooms()
    {
        bool treasuresSpawned = false;

        while (!treasuresSpawned)
        {

            foreach (var room in rt.rooms)
            {
                Instantiate(treasure, new Vector3(Random.Range(room.transform.position.x - 5, room.transform.position.x + 5), Random.Range(room.transform.position.y - 2, room.transform.position.y + 3)), Quaternion.identity);
            }
            treasuresSpawned = true;
            Looter.SetActive(true);
            PathFinderController.SetActive(true);
            // PathFinderController.GetComponent<AstarPath>().Scan();

        }



    }


}
