using UnityEngine;

public class SpawnPoint : MonoBehaviour, IDataPersistenceManager
{
    private static Vector3 position;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            position = this.transform.position;
            // Debug.Log("Saved position in point: " + position.x + " " + position.y + " " + position.z);

            // Обновляем данные через DataPersistenceManager
            GameData data = DataPersistenceManager.instance.GetGameData();
            data.playerPosition = position;
            DataPersistenceManager.instance.SaveGame();
        }
    }


    public void SaveData(GameData data)
    {
        data.playerPosition = position;
        Debug.Log("Saved position in point: " + position.x + " " + position.y + " " + position.z);

    }

    public void LoadData(GameData data)
    {

    }
}