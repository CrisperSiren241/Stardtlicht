using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceScript;

public class CollectibleItem : MonoBehaviour, IInteractable, IDataPersistenceManager
{

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;

    public void Interact()
    {

    }

    public void IsSolved()
    {
        if (!collected)
        {
            collected = true;
            PauseMenu.isAnyMenuOpen = false;
            Debug.Log("IsSolved");
        }

    }

    public void OnFocused() { }
    public void OnDefocused() { }
    public Transform GetTransform()
    {
        if (this == null)
        {
            return null;
        }

        return transform;
    }

    public void LoadData(GameData data)
    {
        data.keysCollected.TryGetValue(id, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.keysCollected.ContainsKey(id))
        {
            data.keysCollected[id] = collected;
            Debug.Log("(data.keysCollected.ContainsKey(id))");
        }
        else
        {
            data.keysCollected.Add(id, collected);
            Debug.Log("data.keysCollected.Add(id, collected)");

        }
    }

}
