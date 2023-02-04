using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameObject monster;
    PlayerController playerController;
    private void Awake()
    {
        monster = FindObjectOfType<MonsterBehavior>().gameObject;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(monster);
        Camera.main.transform.SetParent(null);
        playerController.gameObject.SetActive(false);
    }
}
