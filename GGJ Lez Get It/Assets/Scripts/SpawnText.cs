using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnText : MonoBehaviour
{
    [SerializeField] private List<string> endDialogues;


    private bool hasStarted = false;
    private int index = 0;

    private void Start()
    {

        hasStarted = true;
        
        endDialogues.Add( "You are me.");
        endDialogues.Add("And I am you.");
        endDialogues.Add("It was never our fault.");
        endDialogues.Add("The tree has long been fallen.");
        endDialogues.Add("My brother has long been in peace.");
        endDialogues.Add("I understand now.");
        endDialogues.TrimExcess();
        ReplaceText();
    }


   
    public void ReplaceText()
    {
        if (index >= endDialogues.Capacity)
        {
            SceneManager.LoadScene(sceneName: "MainMenu");

        }
        else
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().text = endDialogues[index];
            index++;
        }
        
    }

}
