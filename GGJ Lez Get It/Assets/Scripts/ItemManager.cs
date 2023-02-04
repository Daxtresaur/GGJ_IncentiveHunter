using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int lureAmount;
    public int LureAmount { get { return lureAmount; } }

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
        GameObject lureClone = Instantiate(lurePrefab, transform.position, Quaternion.identity);
        Destroy(lureClone, 10.0f);
    }
}
