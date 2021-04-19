using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public int desiredFPS = 60;
    public Text closeGame;
    public Image textBkg;
    bool closing = false;

    void Awake()
    {
        Application.targetFrameRate = desiredFPS;
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !closing)
        {
            closing = true;
            closeGame.gameObject.SetActive(true);
            textBkg.gameObject.SetActive(true);
        }

        if (closing && Input.GetKeyDown("y"))
        {
            Application.Quit();
        }

        if (closing && Input.GetKeyDown("n"))
        {
            closing = false;
            closeGame.gameObject.SetActive(false);
            textBkg.gameObject.SetActive(false);
        }
    }
}