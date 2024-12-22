using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SolvePuzzleStep : QuestStep
{
    private int puzzleCollected = 0;
    private int puzzleToComplete = 4;

    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onPuzzleSolved += PuzzleSolved;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onPuzzleSolved -= PuzzleSolved;
    }

    private void PuzzleSolved()
    {
        Debug.Log("Solved Puzzle");
        if (puzzleCollected < puzzleToComplete)
        {
            Debug.Log("Added Puzzle");
            puzzleCollected++;
            UpdateState(); // Добавьте этот вызов здесь
        }

        if (puzzleCollected >= puzzleToComplete)
        {
            FinishQuestStep();
        }
    }


    private void UpdateState()
    {
        string state = puzzleCollected.ToString();
        string status = "Collected " + puzzleCollected + " / " + puzzleToComplete + " puzzles.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.puzzleCollected = System.Int32.Parse(state);
        UpdateState();
    }
}