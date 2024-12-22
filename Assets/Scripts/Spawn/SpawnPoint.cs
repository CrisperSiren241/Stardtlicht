using UnityEngine;

public class SpawnPoint : MonoBehaviour, IDataPersistenceManager
{
    private Vector3 position;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            position = this.transform.position;
            Debug.Log("Saved postion in point: " + position.x + " " + position.y + " " + position.z);
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