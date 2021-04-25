using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFade : MonoBehaviour
{
    public Image fade;
    void Start()
    {
        fade.enabled = true;
        fade.canvasRenderer.SetAlpha(1.0f);
        fade.CrossFadeAlpha(0, 3, true);
        StartCoroutine(DisableCoroutine());
    }

    public void FadeOut(float time)
    {
        fade.enabled = true;
        fade.canvasRenderer.SetAlpha(0);
        fade.CrossFadeAlpha(1, time, true);
    }

    IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(2);

        fade.enabled = false;
    }
}