using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class DayNightCycleController : MonoBehaviour
{
    [Range(0f, 360f)]
    public float lightAngle = 0f;
    public Material skybox;
    public Light mainLight;
    public Color dayLightColor;
    public float dayLightIntensity;
    public Color nightLightColor;
    public float nightLightIntensity;
    public float skyboxRotationSpeed = 1;
    private float skyboxAngle = 0;
    private bool night = false;
    private const int SECONDS_A_DAY = 86400;

    private void Start()
    {
        lightAngle = 360*(float)DateTime.Now.TimeOfDay.TotalSeconds/SECONDS_A_DAY - 120; // "-120": Sun rises at 8:00 AM
        if (lightAngle < 180) skybox.SetFloat("_BlendCubemaps", 1);
        else skybox.SetFloat("_BlendCubemaps", 0);
    }

    void Update()
    {
        if (lightAngle < 180f) {
            if (night) StartCoroutine("BlendSkymapDay");
            mainLight.transform.rotation = Quaternion.Euler(lightAngle, 0, 0);
        }
        else if (lightAngle > 180f) {
            if (!night) StartCoroutine("BlendSkymapNight");
            mainLight.transform.rotation = Quaternion.Euler(lightAngle - 180, 0, 0);
        }
        skybox.SetFloat("_Rotation", skyboxAngle);
        skyboxAngle += skyboxRotationSpeed;
        lightAngle += Time.deltaTime / SECONDS_A_DAY;
        if (lightAngle > 360) lightAngle = 0;
    }

    IEnumerator BlendSkymapNight()
    {
        night = true;
        mainLight.color = nightLightColor;
        mainLight.intensity = nightLightIntensity;
        float value = skybox.GetFloat("_BlendCubemaps");
        while (value > 0) {
            value -= 0.01f;
            skybox.SetFloat("_BlendCubemaps", value);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BlendSkymapDay()
    {
        night = false;
        mainLight.color = dayLightColor;
        mainLight.intensity = dayLightIntensity;
        float value = skybox.GetFloat("_BlendCubemaps");
        while (value < 1)
        {
            value += 0.01f;
            skybox.SetFloat("_BlendCubemaps", value);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
