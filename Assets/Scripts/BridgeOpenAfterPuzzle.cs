using System.Collections;
using UnityEngine;

public class BridgeOpenAfterPuzzle : MonoBehaviour, IDataPersistenceManager
{
    public CollectibleItem puzzle;
    [SerializeField] private string id;

    public float moveSpeed = 2f; 

    private bool isMoving = false;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Start()
    {

    }

    void Update()
    {
        if (puzzle.collected && !isMoving)
        {
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        isMoving = true;
        Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private void MovePlatformInstantly()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void LoadData(GameData data)
    {
        data.bridgeUp.TryGetValue(id, out bool collected);
        if(collected)
        {
            MovePlatformInstantly();
        }
    }

    public void SaveData(GameData data)
    {
        if (data.bridgeUp.ContainsKey(id))
        {
            data.bridgeUp[id] = puzzle.collected;
            
        }
        else
        {
            data.bridgeUp.Add(id, puzzle.collected);
        }
    }
}
