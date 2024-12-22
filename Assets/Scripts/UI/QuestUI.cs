using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text progressText;

    public void UpdateQuestUI(QuestInfoSO questInfo, int currentStepIndex, int totalSteps)
    {
        // Обновление заголовка
        titleText.text = questInfo.displayName;

        // Обновление описания
        descriptionText.text = $"Level Requirement: {questInfo.levelRequirement}\nGold Reward: {questInfo.goldReward}\nExperience Reward: {questInfo.experienceReward}";

        // Обновление прогресса
        progressText.text = $"Progress: Step {currentStepIndex + 1} of {totalSteps}";
    }
}
