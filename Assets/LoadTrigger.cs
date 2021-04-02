using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{
    public GameObject player;
    public string level;
    public Canvas fade;
    public LayerMask layer;
    Vector3 scale;
    Vector3 position;
    Quaternion rotation;

    void Start()
    {
        layer = LayerMask.GetMask("Player");
        scale = this.transform.localScale;
        position = this.transform.position;
        rotation = this.transform.rotation;
    }

    void Update()
    {
        if (Physics.CheckBox(position, scale, rotation, layer))
        {
            fade.GetComponent<LevelFade>().fadeOut();
            player.GetComponent<TimeControl>().resetTime();
            StartCoroutine(LoadLevelCoroutine());
        }
    }

    IEnumerator LoadLevelCoroutine()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }
}