using UnityEngine;

public class SpawnPoint : MonoBehaviour, IDataPersistenceManager
{
    private static Vector3 position;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            position = this.transform.position;
            GameData data = DataPersistenceManager.instance.GetGameData();
            data.playerPosition = position;
            DataPersistenceManager.instance.SaveGame();
        }
    }


    public void SaveData(GameData data)
    {
        data.playerPosition = position;
    }

    public void LoadData(GameData data)
    {

    }
}