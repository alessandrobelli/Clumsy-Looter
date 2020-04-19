using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    public Text treasureText;
    public Text monstersText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        treasureText.text = "Treasures: " + PlayerPrefs.GetInt("lootedTreasures").ToString() + " / " + PlayerPrefs.GetInt("totalTreasures").ToString();
        monstersText.text = "Killed Monsters: " + PlayerPrefs.GetInt("killedMonsters").ToString() + " / " + PlayerPrefs.GetInt("totalMonsters").ToString();
        healthText.text = "Health: " + PlayerPrefs.GetInt("playerHP").ToString() + " / 100";


    }
}
