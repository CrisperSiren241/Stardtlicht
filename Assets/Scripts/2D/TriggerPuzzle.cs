using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{
    public GameObject puzzleCanvas;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleCanvas.SetActive(true);
            CameraService.Instance.Lock();
        }
    }
}
