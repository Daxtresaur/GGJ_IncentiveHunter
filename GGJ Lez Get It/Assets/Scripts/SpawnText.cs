using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    [SerializeField] private List<string> endDialogues;


    private bool hasStarted = false;
    private int index = 0;

    private void Start()
    {

        hasStarted = true;
        ReplaceText();
        endDialogues.Add( "You are me.");
        endDialogues.Add("And I am you.");
        endDialogues.Add("It was never our fault.");
        endDialogues.Add("The tree has long been fallen.");
        endDialogues.Add("My brother has long been in peace.");
        endDialogues.Add("I understand now.");

    }


   
    public void ReplaceText()
    {
        if (index >= endDialogues.Capacity)
        {
            //TODO: return to main menu
            
        }
        else
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().text = endDialogues[index];
            index++;
        }
        
    }

}
