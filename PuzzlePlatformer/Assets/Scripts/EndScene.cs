using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] GameObject sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sceneLoader.GetComponent<SceneLoader>().LoadNextScene();
    }
}
