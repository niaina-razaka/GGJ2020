using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Camera mainCam;
    public float lenght=0.1f;
    public float shakeAmount = 0.1f;

    private void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    public void Shake()
    {
        StartCoroutine("BeginShake");
    }
    private IEnumerator BeginShake()
    {
        float lenghtCoroutine = lenght;
        lenghtCoroutine += Time.time;
        while (Time.time <= lenghtCoroutine)
        {
            Vector3 oldPos= mainCam.transform.position;
            Vector3 camPos = mainCam.transform.position;
            float shakeX = Random.value * shakeAmount;
            float shakeY = Random.value * shakeAmount;

            camPos.x += shakeX;
            camPos.y += shakeY;

            mainCam.transform.position = camPos;

            yield return new WaitForEndOfFrame();

            mainCam.transform.localPosition = oldPos;
         }

    }
}
