using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public float shrinkRate;
    public float maxAngle = 50.0f;
    private Light slight;
    private bool isRunning = false;
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
    }

    public void FixedUpdate()
    {
        DecreaseLight();
    }

    public void DecreaseLight()
    {
        slight.spotAngle -= Time.fixedDeltaTime * shrinkRate;
        slight.spotAngle = Mathf.Clamp(slight.spotAngle, 0.0f, maxAngle);
    }

    public void KillLight()
    {
        slight.spotAngle = 0.0f;
    }

    public void IncreaseLight()
    {
        if (isRunning) return;
        StartCoroutine(GrowLight());
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
