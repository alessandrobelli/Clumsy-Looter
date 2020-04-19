using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Keep : MonoBehaviour
{
    static public bool wasCreated;

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public virtual void Start()
    {
        if (!wasCreated)
        {
            Instantiate(transform.gameObject, transform.position, Quaternion.identity);
            wasCreated = true;
        }
    }

}
