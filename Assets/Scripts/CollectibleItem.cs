using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceScript;

public class CollectibleItem : MonoBehaviour, IInteractable, IDataPersistenceManager
{

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool collected = false;

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
            DataPersistenceManager.instance.SaveGame();
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
        if (data.keysCollected.TryGetValue(id, out bool value))
        {
            collected = value; // Присваиваем сохранённое значение
        }
        else
        {
            Debug.LogWarning($"No data found for {id}");
        }
    }


    public void SaveData(GameData data)
    {
        if (data.keysCollected.ContainsKey(id))
        {
            data.keysCollected[id] = collected;
        }
        else
        {
            data.keysCollected.Add(id, collected);

        }
    }

}
