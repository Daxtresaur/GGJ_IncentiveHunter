using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LittleGirlDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] public HealthComponent health;

    [SerializeField] private List<string> startDialogue;

    [SerializeField] private List<string> Random1;
    [SerializeField] private List<string> Random2;
    [SerializeField] private List<string> Random3;

    [SerializeField] private List<string> dialogueAt75;
    [SerializeField] private List<string> dialogueAt50;
    [SerializeField] private List<string> dialogueAt25;

    [SerializeField] private List<string> afterHiding1;
    [SerializeField] private List<string> afterHiding2;
    [SerializeField] private List<string> afterHiding3;

    [SerializeField] private List<string> closetoEnd;

    private int index = 0;
    private bool endDialogue = false;
    private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.gameObject.SetActive(true);
        showDialogueSet(startDialogue);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Random.Range(1, 3);
        timer += Time.deltaTime;

        if (health.currentHP >= -25)
        {
            showDialogueSet(dialogueAt75);
        }
        else if (health.currentHP >= -50)
        {
            showDialogueSet(dialogueAt50);
        }
        else if (health.currentHP >= -75)
        {
            showDialogueSet(dialogueAt25);
        }
        else if (timer >= 25.0f && timer <= 27.0f)
        {
            showDialogueSet(Random1);
        }
        else if (timer >= 50.0f && timer <= 52.0f)
        {
            showDialogueSet(Random2);
        }
        else if (timer >= 75.0f && timer <= 77.0f)
        {
            showDialogueSet(Random3);
        }


    }

    public void showDialogueSet(List<string> stringList)
    {
        if (stringList != null)
        {
            setDialogueText(stringList, stringList[index]);
        }
        else
        {
            Debug.Log("list null");
        }

        
    }

    private void setDialogueText(List<string> stringList, string text)
    {
        if (!endDialogue)
        {
            if (index % 2 == 0)
            {
                textMeshProUGUI.color = Color.white;
            }
            else
            {
                textMeshProUGUI.color = Color.red;
            }
            textMeshProUGUI.text = text;
            index++;

            if (index < stringList.Count)
            {
                StartCoroutine(WaitText(stringList, stringList[index]));
            }
            else
            {
                StartCoroutine(WaitLastText(stringList, stringList[index-1]));
                
            }

        }
        else
        {
            index = 0;
        }

    }

    private void setLastDialogueText(List<string> stringList, string text)
    {
        textMeshProUGUI.text = text;
        index = 0;
    }

    IEnumerator WaitText(List<string> stringList, string text)
    {
        //Debug.Log("bruh");
        yield return new WaitForSeconds(1.5f);
        setDialogueText(stringList, text);

    }

    IEnumerator WaitLastText(List<string> stringList, string text)
    {
        //Debug.Log("bruh");
        yield return new WaitForSeconds(1.5f);
        setLastDialogueText(stringList, text);
        textMeshProUGUI.gameObject.SetActive(false);
        endDialogue = true;

    }


}
