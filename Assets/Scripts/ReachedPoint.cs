using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameEventsManager.instance.miscEvents.DestinationReached();
    }
}
