using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private AudioClip stoneThrow;
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private int lureAmount;
    public int LureAmount 
    { 
        get { return lureAmount; }
        set 
        { 
            lureAmount = value;

        }
    }

    [SerializeField] private GameObject lurePrefab;

    public void OnThrow()
    {
        Debug.Log("Throw");
        ThrowLure();
    }

    public void ThrowLure()
    {
        if (lureAmount <= 0) return;

        if (lurePrefab == null) return;
        lureAmount--;
        m_TextMeshPro.SetText($"MONSTER LURE: {lureAmount}");
        GameObject lureClone = Instantiate(lurePrefab, transform.position, Quaternion.identity);
        SoundManager.instance.PlaySFX(stoneThrow);
        Destroy(lureClone, 10.0f);
    }

    public void GetLure()
    {
        lureAmount++;
        lureAmount = Mathf.Clamp(lureAmount, 0, 3);
    }
}
