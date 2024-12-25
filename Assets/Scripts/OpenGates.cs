using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGates : MonoBehaviour, IDataPersistenceManager
{
    public string questId;
    private bool isNear = false;
    void Start()
    {

    }

    void Update()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        QuestState currentQuestState = questManager.CheckQuestState(questId);

        if (isNear)
        {
            SceneManager.LoadSceneAsync("Level2");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        isNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        isNear = false;
    }

    public void SaveData(GameData data)
    {
        data.Level = SceneManager.GetActiveScene().name;
    }
    public void LoadData(GameData data) { }
}
