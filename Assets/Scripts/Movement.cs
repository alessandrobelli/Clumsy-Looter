using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region Variables
    public int speed;
    public Camera mainCamera;
    public enum PlayerDirection
    {
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }


    PlayerDirection direction;
    private float t_x;
    private float t_y;
    private Transform t_position;
    #endregion


    #region Monobehaviour Callbacks
    // Start is called before the first frame update
    void Start()
    {
        t_position = this.transform;
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        transform.position = transform.position + horizontal * Time.deltaTime * speed;




    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {





        }
    }

    #endregion
}
