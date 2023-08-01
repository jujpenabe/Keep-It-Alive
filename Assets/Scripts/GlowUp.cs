using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class GlowUp : MonoBehaviour
{
    // Get Light 2D Component and increase intensity
    private Light2D light2D;
    
    public float maxIntensity = 1f;
   
    
    void Awake()
    {
        light2D = GetComponent<Light2D>();
    }
    
    public void GlowUpIntensity()
    {
        StartCoroutine(IncreaseIntensity());
    }
    IEnumerator IncreaseIntensity(){
        while (light2D.intensity<maxIntensity){
            yield return null;
            light2D.intensity += 0.01f;
        }
    }
}
