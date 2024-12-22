using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestinationStep : QuestStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onDestinationReached += ReachDestination;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onDestinationReached -= ReachDestination;
    }

    private void ReachDestination()
    {
        FinishQuestStep();
    }

    protected override void SetQuestStepState(string state)
    {
        throw new System.NotImplementedException();
    }
}
