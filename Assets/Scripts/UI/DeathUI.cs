using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour, IDataPersistenceManager
{
    public CharacterStats stats;
    public GameObject deathUI;
    private string scene;
    void Start()
    {
        deathUI.SetActive(false);
    }

    void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDeath += DisplayPanel;
    }

    void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDeath -= DisplayPanel;

    }

    void DisplayPanel()
    {
        if (PauseMenu.isAnyMenuOpen) return;
        deathUI.SetActive(true);
        CameraService.Instance.Lock();
        PauseMenu.isAnyMenuOpen = true;
    }

    public void Restart()
    {
        DataPersistenceManager.instance.SaveGame();   
        stats.isDead = false;
        stats.currentHealth = stats.maxHealth;
        CameraService.Instance.UnLock();
        deathUI.SetActive(false);
        PauseMenu.isAnyMenuOpen = false;
        if (QuestManager.Instance != null)
        {
            foreach (Quest quest in QuestManager.Instance.questMap.Values)
            {
                QuestManager.Instance.SaveQuest(quest);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveData(GameData data) { }
    public void LoadData(GameData data)
    {
        scene = data.Level;
    }
}
