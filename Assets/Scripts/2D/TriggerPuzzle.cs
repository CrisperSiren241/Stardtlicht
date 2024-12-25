using UnityEngine;

public class TriggerPuzzle : MonoBehaviour, IDataPersistenceManager
{
    public GameObject puzzleCanvas;
    public string questId;
    private bool isPlayerInTrigger;
    private bool isSolved = false;

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

    public void LoadData(GameData data)
    {
        CollectibleItem item = GetComponent<CollectibleItem>();
        data.keysCollected.TryGetValue(item.id, out isSolved);
        if (isSolved)
        {
            Destroy(puzzleCanvas);
        }
    }
    public void SaveData(GameData data) { }
}
