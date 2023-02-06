using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOnEnter;
    [SerializeField] private bool playOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!playOnEnter) return;
        SoundManager.instance.PlaySFX(clip);

#if false
        int index = Random.Range(1, 3);
        if (FindObjectOfType<LittleGirlDialogue>() == null)
        {
            Debug.Log("henlo");
        }
        if (index == 1)
        {
            FindObjectOfType<LittleGirlDialogue>().showDialogueSet(FindObjectOfType<LittleGirlDialogue>().afterHiding1);
        }
        else if (index == 2)
        {
            FindObjectOfType<LittleGirlDialogue>().showDialogueSet(FindObjectOfType<LittleGirlDialogue>().afterHiding2);
        }
        else if (index == 3)
        {
            FindObjectOfType<LittleGirlDialogue>().showDialogueSet(FindObjectOfType<LittleGirlDialogue>().afterHiding3);
        }
#endif
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!playOnExit) return;
        SoundManager.instance.PlaySFX(clip);
    }
}
