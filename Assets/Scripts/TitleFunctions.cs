using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleFunctions : MonoBehaviour
{
    public AudioSource music;
    public AudioClip slowClip;
    public GameObject colorFilter;
    public GameObject videoObj;
    public Canvas fade;

    ChromaticAberration chromaLayer = null;
    ColorGrading colorLayer = null;
    PostProcessVolume volume;
    AudioSource audioData;
    VideoPlayer videoPlayer;

    int startTimeEffect = 0;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.clip = slowClip;

        volume = colorFilter.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorLayer);
        volume.profile.TryGetSettings(out chromaLayer);
        videoPlayer = videoObj.GetComponent<VideoPlayer>();
    }

    void Update()
    {
        chromaLayer.intensity.value = -Time.timeScale + 1;

        videoPlayer.playbackSpeed = Time.timeScale * 1.5f;

        colorLayer.hueShift.value = ((Time.timeScale * 180) - 180);

        if (Time.timeScale > 0 && Time.timeScale < .25)
        {
            colorLayer.saturation.value = ((Time.timeScale * 400) - 100);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        startTimeEffect++;
        //Prevent the effect from being spammed or stacked

        if (startTimeEffect == 1)
        {
            StartCoroutine(LerpTime(.1f, 1f, 1));
            StartCoroutine(LoadLevelCoroutine());

            fade.GetComponent<LevelFade>().FadeOut(3);

            audioData.Play();
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
            music.pitch = Time.timeScale;
            yield return null;
        }
        Time.timeScale = final;
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(1);
    }
}