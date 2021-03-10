using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    public Image fade;
    void Start()
    {
        fade.enabled = true;
        fade.canvasRenderer.SetAlpha(1.0f);
        fade.CrossFadeAlpha(0, 2, false);
    }

    public void fadeOut()
    {
        fade.CrossFadeAlpha(1, 1, false);
    }
}