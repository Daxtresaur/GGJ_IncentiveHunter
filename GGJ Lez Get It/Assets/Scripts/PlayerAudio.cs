using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip footStep;
    private WaitForSeconds slowFootStepsInterval = new(2.0f);
    private WaitForSeconds walkFootStepsInterval = new(1.0f);
    private WaitForSeconds runFootStepsInterval = new(0.6f);

    bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
