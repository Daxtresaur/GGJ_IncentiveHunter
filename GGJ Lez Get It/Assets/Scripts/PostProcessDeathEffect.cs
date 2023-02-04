using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessDeathEffect : MonoBehaviour
{
    public PostProcessVolume volume;
    // properties of class
    public float bloom = 10f;
    public float saturation = 10f;

    public Bloom bloomLayer = null;
    public AmbientOcclusion ambientOcclusionLayer = null;
    public ColorGrading colorGradingLayer = null;

   
    // Start is called before the first frame update
    void Start()
    {
        // somewhere during initializing
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out ambientOcclusionLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);

        ambientOcclusionLayer.enabled.value = true;

        bloomLayer.enabled.value = true;
        bloomLayer.intensity.value = bloom;

        colorGradingLayer.enabled.value = true;
        colorGradingLayer.saturation.value = saturation;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
