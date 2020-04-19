using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Keep
{

    public RoomTemplates rt;
    public GameObject PathFinderController;
    public bool loading = true;

    public GameObject Looter;
    public GameObject Player;
    public GameObject treasure;
    public GameObject[] monsters;
    public int numberOfMonstersInEachRoom;
    public GameObject winText;

    public GameObject loadingImage;
    public float x_left = 0;
    public float x_right = 0;
    public float y_top = 0;
    public float y_bottom = 0;

    private int totalMonsters;
    private int totalTreasures;
    public Canvas gui;

    public override void Start()
    {

        loadingImage.SetActive(true);
        rt = GameObject.Find("Entry Room").GetComponent<RoomTemplates>();
        Invoke("populateRooms", rt.waitTime);




    }

    void populateRooms()
    {

        while (loading)
        {

            foreach (var room in rt.rooms)
            {
                if (room.transform.position.x > x_right) x_right = room.transform.position.x;
                else if (room.transform.position.x < x_left) x_left = room.transform.position.x;

                if (room.transform.position.y > y_top) y_top = room.transform.position.y;
                else if (room.transform.position.y < y_bottom) y_bottom = room.transform.position.y;

                GameObject justSpawnedTreasure = Instantiate(treasure, new Vector3(Random.Range(room.transform.position.x - 5, room.transform.position.x + 5), Random.Range(room.transform.position.y - 2, room.transform.position.y + 2)), Quaternion.identity);

                for (int i = 0; i < numberOfMonstersInEachRoom; i++)
                {
                    Instantiate(monsters[Random.Range(0, monsters.Length)], new Vector3(justSpawnedTreasure.transform.position.x + Random.Range(1, 2), justSpawnedTreasure.transform.position.y + Random.Range(1, 2)), Quaternion.identity);
                    totalMonsters++;
                }

                totalTreasures++;

            }


            loadingImage.SetActive(false);
            Looter.SetActive(true);

            ResizePathFinder();
            Player.SetActive(true);
            gui.gameObject.SetActive(true);
            Camera.main.GetComponent<Snapping>().player = Player;
        }

        PlayerPrefs.SetInt("totalTreasures", totalTreasures);
        PlayerPrefs.SetInt("totalMonsters", totalMonsters);



    }

    private void Update()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            var scene = SceneManager.GetSceneAt(i);

            if (scene.name == "BattleScene")
            {
                PathFinderController.SetActive(false);
                gui.gameObject.SetActive(false);

            }
            else
            {
                PathFinderController.SetActive(true);
                gui.gameObject.SetActive(true);
            }
        }

        if(PlayerPrefs.GetInt("lootedTreasures") == PlayerPrefs.GetInt("totalTreasures"))
        {
            
            winText.SetActive(true);
            LoadLevelAfterDelay(7f);
        }

    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }

    private void ResizePathFinder()
    {
        int width = (int)x_right + (int)Mathf.Abs(x_left) + 20;
        int depth = (int)y_top + (int)Mathf.Abs(y_bottom) + 20;
        float nodeSize = 1;

        PathFinderController.SetActive(true);
        DontDestroyOnLoad(PathFinderController);
        AstarData data = PathFinderController.GetComponent<AstarPath>().data;
        GridGraph gg = data.graphs[0] as GridGraph;

        gg.center.x = (x_right + x_left) / 2;
        gg.center.y = (y_top + y_bottom) / 2;
        gg.neighbours = NumNeighbours.Four;
        gg.uniformEdgeCosts = true;
        gg.inspectorGridMode = InspectorGridMode.Grid;
        gg.SetDimensions(width, depth, nodeSize);

        AstarPath.active.Scan();
        loading = false;
    }
}
