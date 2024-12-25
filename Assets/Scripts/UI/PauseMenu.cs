using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool isAnyMenuOpen = false;
    public GameObject pausePanel;
    // Update is called once per frame

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        isAnyMenuOpen = false;
    }

    void Pause()
    {
        if (isAnyMenuOpen) return;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        isAnyMenuOpen = true;
    }


    public void toMainMenu()
    {
        // Проверяем, существует ли QuestManager, и сохраняем квесты
        if (QuestManager.Instance != null)
        {
            foreach (Quest quest in QuestManager.Instance.questMap.Values)
            {
                QuestManager.Instance.SaveQuest(quest);
            }
        }

        // Возвращаем время в нормальный режим
        GameIsPaused = false;
        Time.timeScale = 1f;

        // Переход на сцену главного меню
        SceneManager.LoadSceneAsync("Menu");
    }

}
