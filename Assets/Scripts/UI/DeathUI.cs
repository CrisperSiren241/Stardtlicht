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
        deathUI.SetActive(true);
        CameraService.Instance.Lock();
    }
    public void Restart()
    {
        stats.isDead = false;
        stats.currentHealth = stats.maxHealth; // Полностью восстанавливаем здоровье
        CameraService.Instance.UnLock();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void SaveData(GameData data) { }
    public void LoadData(GameData data)
    {
        scene = data.Level;
    }
}
