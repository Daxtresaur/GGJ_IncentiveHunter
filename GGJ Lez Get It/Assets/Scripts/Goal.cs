using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject canvas;
    public blackout Blackout;
    GameObject monster;
    PlayerController playerController;
    private void Awake()
    {
        canvas.SetActive(false);
        monster = FindObjectOfType<MonsterBehavior>().gameObject;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        canvas.SetActive(true);
        Blackout.FadeWin();
        Destroy(monster);
        Camera.main.transform.SetParent(null);
        playerController.gameObject.SetActive(false);
    }
}
