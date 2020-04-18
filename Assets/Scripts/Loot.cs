using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Loot : MonoBehaviour
{
    private Pathfinding.AIDestinationSetter pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding.AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTreasure();
    }

    private void FindClosestTreasure()
    {
        float distanceToClosestTreasure = Mathf.Infinity;
        Treasure closestTreasure = null;
        Treasure[] allTreasures = GameObject.FindObjectsOfType<Treasure>();

        foreach (Treasure treasure in allTreasures)
        {
            float distanceToTreasure = (treasure.transform.position - transform.position).sqrMagnitude;
            if (distanceToTreasure < distanceToClosestTreasure)
            {
                distanceToClosestTreasure = distanceToTreasure;
                closestTreasure = treasure;
                pathfinding.target = treasure.transform;
            }
        }

        Debug.DrawLine(transform.position, closestTreasure.transform.position);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Loot"))
        {
            Debug.LogWarning("destroy");
            Destroy(other.gameObject);
        }
    }

}
