using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Looter : Keep
{
    public AIPath aIPath;
    private Pathfinding.AIDestinationSetter pathfinding;
    static public int MaxHealth = 100;
    public int CurrentHealth;
    public AudioClip damagedClip;
    private AudioSource audioPlayer;

    public int lootedTreasure;

    public override void Start()
    {
        base.Start();
        pathfinding = GetComponent<Pathfinding.AIDestinationSetter>();
        InvokeRepeating("FindClosestTreasure", 0f, 3f);

        

        CurrentHealth = MaxHealth;
        PlayerPrefs.SetInt("playerHP", CurrentHealth);
        audioPlayer = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);

        }






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

        //  Debug.DrawLine(transform.position, closestTreasure.transform.position);


    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {
            CurrentHealth -= other.gameObject.GetComponent<Monster>().attack;
            PlayerPrefs.SetInt("playerHP", CurrentHealth);
            audioPlayer.PlayOneShot(damagedClip);
            yield return new WaitForSeconds(2);
        }
    }

}
