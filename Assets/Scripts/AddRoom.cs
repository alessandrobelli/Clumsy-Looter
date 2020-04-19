using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : Keep
{

    private RoomTemplates templates;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        templates = GameObject.FindGameObjectWithTag("Room").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
