using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeControl : MonoBehaviour
{
    AudioSource audioData;
    public AudioSource music;
    public AudioClip slowClip;
    public AudioClip fastClip;
    public GameObject colorFilter;
    PostProcessVolume volume;

    ChromaticAberration chromaLayer = null;
    ColorGrading colorLayer = null;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        volume = colorFilter.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorLayer);
        volume.profile.TryGetSettings(out chromaLayer);
    }

    void Update()
    {
        chromaLayer.intensity.value = -Time.timeScale + 1;
        colorLayer.hueShift.value = ((Time.timeScale * 180) - 180);

        if (Time.timeScale > 0 && Time.timeScale < .25)
        {
            colorLayer.saturation.value = ((Time.timeScale * 400) - 100);
        }

        if (Input.GetKeyDown("e"))
        {
            if (Time.timeScale == 1)
            {
                StartCoroutine(LerpTime(.1f, 1f, 0f));
                audioData.clip = slowClip;
                audioData.Play();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 0.1f;
                StartCoroutine(LerpTime(.9f, 1f, 1f));
                audioData.clip = fastClip;
                audioData.Play();
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
            yield return null;
            music.pitch = Time.timeScale;
        }
        Time.timeScale = final;
        music.pitch = final;
    }
}