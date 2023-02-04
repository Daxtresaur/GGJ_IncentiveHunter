using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public float shrinkRate;
    public float maxAngle = 50.0f;
    public float biggerAngle = 150.0f;
    private Light slight;
    private bool isRunning = false;

    private float targetSize;
    public bool LightIsGone
    {
        get
        {
            return slight.spotAngle <= 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        slight = GetComponent<Light>();
        slight.spotAngle = maxAngle;
        targetSize = maxAngle;
    }

    public void FixedUpdate()
    {
        //DecreaseLight();
        slight.spotAngle = Mathf.Lerp(slight.spotAngle, targetSize, Time.deltaTime);
    }

    public void DecreaseLight()
    {
        //slight.spotAngle = maxAngle;
        targetSize = maxAngle;
        //slight.spotAngle -= Time.fixedDeltaTime * shrinkRate;
        //slight.spotAngle = Mathf.Clamp(slight.spotAngle, 0.0f, maxAngle);
    }

    public void KillLight()
    {
        //slight.spotAngle = 0.0f;
        targetSize = 0.0f;
        //slight.spotAngle = 0.0f;
    }

    public void IncreaseLight()
    {
        //slight.spotAngle = maxAngle * 2.0f;
        targetSize = biggerAngle;
        //if (isRunning) return;
        //StartCoroutine(GrowLight());
    }

    IEnumerator GrowLight()
    {
        if(isRunning) yield break;
        isRunning = true;
        while (slight.spotAngle < maxAngle)
        {
            slight.spotAngle += 10.0f * Time.fixedDeltaTime;
            yield return null;
        }
        slight.spotAngle = Mathf.Clamp(slight.spotAngle, 0.0f, maxAngle);
        yield return null;
        isRunning = false;
    }
}
