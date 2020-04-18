using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    public Vector2[] edges;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        if (edges.Length > 0)
        {
            if (player.transform.localPosition.y > edges[0].y)
            {
                transform.position = transform.position + new Vector3(0, 10);
                edges[2].y = edges[0].y;
                edges[0].y += 10f;

            }
            else if (player.transform.localPosition.y < edges[2].y)
            {
                transform.position = transform.position + new Vector3(0, -10);
                edges[0].y = edges[2].y;
                edges[2].y -= 10f;
            }

            if (player.transform.localPosition.x > edges[1].x)
            {
                transform.position = transform.position + new Vector3(18, 0);
                edges[3].x = edges[1].x;
                edges[1].x += 18f;

            }
            else if (player.transform.localPosition.x < edges[3].x)
            {
                transform.position = transform.position + new Vector3(-18, 0, 0);
                edges[1].x = edges[3].x;
                edges[3].x -= 18f;
            }

        }
    }

}
