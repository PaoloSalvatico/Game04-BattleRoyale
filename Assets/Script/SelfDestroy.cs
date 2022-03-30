using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public int delay = 2;
    void Start()
    {
        Destroy(gameObject, delay);
    }

}
