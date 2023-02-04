using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destructionTime = 3.0f;
    public Transform playerPos;

    void Awake()
    {
        this.gameObject.transform.position = playerPos.position;
        this.gameObject.transform.position -= new Vector3(-3.5f, 0, 3);
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }
}
