using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Debug.Log(other.tag);
            Destroy(other.gameObject);
        }
    }
}
