using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour, IDataPersistenceManager
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync("Level2");
    }

    public void SaveData(GameData data){
        data.Level = SceneManager.GetActiveScene().name;
    }
    public void LoadData(GameData data){}
}
