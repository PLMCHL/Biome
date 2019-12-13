using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightBehaviour : MonoBehaviour
{
    private Light sun;
    private float secondsInFullDay = 120f;
    [Range(0, 1)]
    private float currentTimeOfDay = 0.25f;
    [HideInInspector]
    private float timeMultiplier = 1f;

    float sunInitialIntensity;

    void Start()
    {
        sun = this.transform.GetChild(0).GetComponent<Light>(); ;
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, (currentTimeOfDay * 360f) - 90);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        print(intensityMultiplier + " : " + currentTimeOfDay);
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
