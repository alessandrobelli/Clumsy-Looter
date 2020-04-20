using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Keep
{

    #region Variables
    public int speed;
    public Camera mainCamera;
    static public int MaxHealth = 100;
    private SpriteRenderer spriteRenderer;
    public int killedMonsters = 0;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        PlayerPrefs.SetInt("InBattle", 0);

        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("InBattle") == 0)
        {
            Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            transform.position = transform.position + horizontal * Time.deltaTime * speed;

            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") > 0);
        }




    }

    #endregion
}
