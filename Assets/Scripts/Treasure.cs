using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Keep
{
    public Sprite openedTreasure;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Looter"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = openedTreasure;
            Destroy(gameObject, 4f);
            other.GetComponent<Looter>().lootedTreasure++;
            PlayerPrefs.SetInt("lootedTreasures", other.GetComponent<Looter>().lootedTreasure);
        }
    }
}
