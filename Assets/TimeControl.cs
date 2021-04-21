using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    AudioSource audioData;
    public AudioSource music;
    public AudioClip slowClip;
    public AudioClip starClip;
    public AudioClip worldClip;
    public AudioClip fastClip;
    public GameObject colorFilter;
    public RawImage stopWatchArm;
    public RawImage timeScaleArm;
    public Text stopWatchText;
    PostProcessVolume volume;

    ChromaticAberration chromaLayer = null;
    ColorGrading colorLayer = null;

    float countdown = 12;
    bool loading = false;
    int random;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        volume = colorFilter.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorLayer);
        volume.profile.TryGetSettings(out chromaLayer);
        stopWatchText.text = "12";
    }

    void Update()
    {
        chromaLayer.intensity.value = -Time.timeScale + 1;
        //Chromatic Aberration gives the visual distortion effect around the edges of the camera

        colorLayer.hueShift.value = ((Time.timeScale * 180) - 180);
        //Hue Shift is, as the name implies, a shift in the hue (or color). Causing a rainbow color effect

        if (Time.timeScale > 0 && Time.timeScale < .25)
        {
            colorLayer.saturation.value = ((Time.timeScale * 400) - 100);
        }
        //Color Saturation gives everything its color. Lowering the saturation makes everything become monochrome

        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown("e") && countdown > 5)
            {
                StartCoroutine(LerpTime(.1f, 1f, 0f));

                random = Random.Range(0, 100);
                audioData.pitch = 1;

                if (random < 5)
                {
                    audioData.clip = starClip;
                }
                else if (random >= 95)
                {
                    audioData.clip = worldClip;
                }
                else
                {
                    audioData.clip = slowClip;
                    audioData.pitch = 1.3f;
                }
                audioData.Play();

                //Random chance to play an alternate time stop sound effect (Easter Egg)
            }
            /**
             * If the player presses E AND time is currently moving at full speed
             * AND the stop watch time is greater than 5:
             * Begin the slow down effect
             */

            if (countdown < 11.9)
            {
                countdown += Time.unscaledDeltaTime;
                UpdateUI(false);

            }
            else if (countdown != 12)
            {
                countdown = 12;
                UpdateUI(false);
            }

        }
        else if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown("e") || countdown == 0)
            {
                Time.timeScale = 0.1f;
                StartCoroutine(LerpTime(.9f, 1f, 1f));
                audioData.clip = fastClip;
                audioData.pitch = 1.3f;
                audioData.Play();
            }
            /**
             * (If the player presses E AND time is currently stopped)
             * OR the stop watch time is 0:
             * Begin the speed up effect
             */

            if (countdown > 0)
            {
                countdown -= Time.unscaledDeltaTime;
                UpdateUI(true);
            }
            else if (countdown < 0)
            {
                countdown = 0;
                UpdateUI(true);
            }
        }
    }
    IEnumerator LerpTime(float _lerpTimeTo, float _timeToTake, float final)
    {
        float endTime = Time.time + _timeToTake;
        float startTimeScale = Time.timeScale;
        float startArm = timeScaleArm.transform.rotation.z;
        
        float i = 0f;        
        while (Time.time < endTime && !loading)
        {
            i += (1 / _timeToTake) * Time.deltaTime;
            Time.timeScale = Mathf.Lerp(startTimeScale, _lerpTimeTo, i);
            music.pitch = Time.timeScale;
            stopWatchText.text = "?";
            timeScaleArm.transform.rotation = Quaternion.Euler(0, 0, (Time.timeScale* 1800) + startArm);
            yield return null;
        }

        if (!loading)
        {
            Time.timeScale = final;
            music.pitch = final;
        }
    }
    //Slowly increase or decrease the time scale

    void UpdateUI(bool flip)
    {
        if (countdown > 4)
        {
            stopWatchText.color = new Color32(0, 255, 5, 255);
        }
        else
        {
            stopWatchText.color = new Color32(255, 255, 255, 255);
        }

        if (flip)
        {
            stopWatchText.text = (Mathf.Floor(countdown)).ToString();
        }
        else
        {
            stopWatchText.text = (Mathf.Ceil(countdown)).ToString();
        }
        stopWatchArm.transform.rotation = Quaternion.Euler(0, 0, (countdown / 12 * 360) + 45);
    }
    //Update the stop watch UI elements

    public void resetTime()
    {
        loading = true;
        Time.timeScale = 1;
    }
    //Special method used to halt all time-based effects.
    //Specifically needed for level transitions.
}