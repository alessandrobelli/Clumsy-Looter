using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rollingText : MonoBehaviour
{
    private Text text;
    private string[] phrases = { 
        "...Gatering enough monsters...", 
        "...Getting enough naked...", 
        "...Going down the stairs...", 
        "... Petting the monsters ...",
        "... Preparing the food ...",
        "... Checking if we really need to do it ...",
        "... Why again are we doing this? "
    
    };
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("changeText", 0.1f, 1f);
    }

    private void changeText()
    {
        text.text = phrases[Random.Range(0, phrases.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
