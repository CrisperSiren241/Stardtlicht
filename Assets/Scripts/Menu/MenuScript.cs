using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("Level1");
    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        string lastScene = DataPersistenceManager.instance.GetGameData().Level;
        SceneManager.LoadSceneAsync(lastScene);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
