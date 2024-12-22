using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync("Level2");
    }
}
