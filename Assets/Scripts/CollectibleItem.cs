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

    private bool collected = false; // Флаг для отслеживания, собран ли предмет

    public void Interact()
    {
        if (!collected)
        {
            collected = true; // Помечаем предмет как собранный
            Destroy(gameObject); // Удаляем предмет из сцены
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
        // Обновляем или добавляем статус предмета
        if (data.keysCollected.ContainsKey(id))
        {
            data.keysCollected[id] = collected; // Обновляем значение
        }
        else
        {
            data.keysCollected.Add(id, collected); // Добавляем новый ключ
        }
    }

}
