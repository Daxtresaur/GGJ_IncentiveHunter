using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPreStartText : MonoBehaviour
{
    [SerializeField] private List<string> startListDialogues;


    private bool hasStarted = false;
    private int index = 0;

    private void Start()
    {
        index = 0;
        hasStarted = true;
        startListDialogues.Add("I’ve been fighting you");
        startListDialogues.Add("Ever since that day");
        startListDialogues.Add("When the tree I loved to climb upon fell");
        startListDialogues.Add("Why must he fall as well?");
        startListDialogues.Add("I was there with him.");
        startListDialogues.Add("Why not me?");
        startListDialogues.TrimExcess();
        ReplaceText();
        

    }



    public void ReplaceText()
    {
        if (index >= startListDialogues.Capacity)
        {
            Debug.Log("index"+index);
            SceneManager.LoadScene(sceneName: "Scene");

        }
        else
        {
            Debug.Log("index" + index + "/ max: " + startListDialogues.Capacity);
            this.gameObject.GetComponent<TextMeshProUGUI>().text = startListDialogues[index];
            index++;
        }

    }
}
