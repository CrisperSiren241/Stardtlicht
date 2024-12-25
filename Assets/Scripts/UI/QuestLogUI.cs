using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        // GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        // GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestLogTogglePressed()
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    void Update()
    {
        
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void QuestStateChange(Quest quest)
    {
        SetQuestLogInfo(quest);
    }

    private void SetQuestLogInfo(Quest quest)
    {
        if (quest.state == QuestState.IN_PROGRESS || quest.state == QuestState.CAN_FINISH)
        {
            questDisplayNameText.text = quest.info.displayName;
        }
        else{
             questDisplayNameText.text = "";
        }
        questStatusText.text = quest.GetFullStatusText();
    }
}