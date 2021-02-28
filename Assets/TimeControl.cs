using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{

    AudioSource audioData;
    public AudioSource music;
    public AudioClip slowClip;
    public AudioClip fastClip;
    bool beginSlow = false;
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (Time.timeScale == 1)
            {
                StartCoroutine(LerpTime(.1f, 1f, 0f));
                audioData.clip = slowClip;
                audioData.Play();
                music.Pause();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 0.1f;
                StartCoroutine(LerpTime(.9f, 1f, 1f));
                audioData.clip = fastClip;
                audioData.Play();
                music.UnPause();
            }
        }
    }
    IEnumerator LerpTime(float _lerpTimeTo, float _timeToTake, float final)
    {
        float endTime = Time.time + _timeToTake;
        float startTimeScale = Time.timeScale;
        float i = 0f;
        while (Time.time < endTime)
        {
            i += (1 / _timeToTake) * Time.deltaTime;
            Time.timeScale = Mathf.Lerp(startTimeScale, _lerpTimeTo, i);
            print(Time.timeScale);
            yield return null;
        }
        Debug.Log("Done");
        Time.timeScale = final;
    }
}