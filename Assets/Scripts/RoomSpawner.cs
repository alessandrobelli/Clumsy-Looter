using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : Keep
{

    # region Variables

    // 1 bottom 2 top 3 left 4 right
    public enum Opening
    {
        Bottom = 1,
        Top = 2,
        Left = 3,
        Right = 4

    }
    public Opening openingDirection;
    public bool spawned = false;

    public float waitTime = 0.2f;

    private RoomTemplates templates;
    private int rand;
    #endregion

    #region Monobehavior Callbacks;
    // Start is called before the first frame update
    public override void Start()
    {


        Destroy(gameObject, waitTime);
        Destroy(GameObject.Find("Destroyer"), waitTime);
        templates = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);


    }
    #endregion

    // Update is called once per frame
    void Spawn()
    {
        if (spawned == false)
        {

            GameObject room = null;
            switch (openingDirection)
            {
                case Opening.Bottom:
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    room = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);

                    break;
                case Opening.Top:
                    rand = Random.Range(0, templates.topRooms.Length);
                    room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    break;
                case Opening.Left:
                    rand = Random.Range(0, templates.leftRooms.Length);
                    room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    break;
                case Opening.Right:
                    rand = Random.Range(0, templates.rightRooms.Length);
                    room = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try
            {
                if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
                {
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("e");
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
