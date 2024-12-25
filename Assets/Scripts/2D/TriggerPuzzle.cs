using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{
    public GameObject puzzleCanvas;
    public string questId;
    private bool isPlayerInTrigger;

    void Update()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        QuestState currentQuestState = questManager.CheckQuestState(questId);
        if (puzzleCanvas != null && Input.GetKeyDown(KeyCode.E) && currentQuestState == QuestState.IN_PROGRESS && isPlayerInTrigger)
        {
            TriggerThisPuzzle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        isPlayerInTrigger = false;
    }

    void TriggerThisPuzzle()
    {
        puzzleCanvas.SetActive(true);
        CameraService.Instance.Lock();
        PauseMenu.isAnyMenuOpen = true;
    }
}
