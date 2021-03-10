using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{
    public string level;
    public Canvas fade;

    void OnTriggerEnter(Collider coll)
    {
        fade.GetComponent<LevelFade>().fadeOut();
        StartCoroutine(LoadLevelCoroutine());
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }
}