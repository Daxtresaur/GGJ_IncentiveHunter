using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destructionTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }
}