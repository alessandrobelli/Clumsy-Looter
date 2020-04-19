using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : Keep
{

    static public int MaxHealth = 100;
    public int CurrentHealth;
    public int attack;
    public int level;

    public string[] monsterNames = {
"Shadowmirage",
"Slagbrute",
"Caveghoul",
"Volatile Blob",
"Tall Troglodyte",
"Lonely Deviation",
"Aquatic Preying Hound",
"Tattooed Sun Hound",
"Masked Dawn",
"Tombhood",
"Chaoshand",
"Bowelfiend",
"Fogfang",
"Evil Revenant",
"Enraged Charmer",
"Dirty Statue",
"Stormcloud Dire Monster",
"A full friday"
};

    public override void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        CurrentHealth = MaxHealth;
        attack = Random.Range(8, 20);
        level = (int)Remap(attack, 8, 20, 1, 100);


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            PlayerPrefs.SetInt("monsterAttack", attack);
            PlayerPrefs.SetInt("monsterLevel", level);
            PlayerPrefs.SetString("monsterName", monsterNames[Random.Range(0, monsterNames.Length - 1)]);
            if (gameObject.transform.name.Replace("(Clone)", "") == "GreenMonster") PlayerPrefs.SetString("monsterPrefab", "GreenMonster");
            else if (gameObject.transform.name.Replace("(Clone)", "") == "PurpleMonster") PlayerPrefs.SetString("monsterPrefab", "PurpleMonster");
            PlayerPrefs.SetInt("playerHP", GameObject.Find("Looter").GetComponent<Looter>().CurrentHealth);

            Camera.main.GetComponent<AudioSource>().Stop();
            GameObject.Find("Looter").GetComponent<Looter>().aIPath.SetPath(null);
            GameObject.Find("Looter").GetComponent<Looter>().aIPath.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
            Destroy(gameObject);

        }

        if (other.CompareTag("Looter"))
        {
            // damage??
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
