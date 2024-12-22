using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector2 objectInitPos;
    public GameObject objectToDrag;
    public GameObject ObjectDragToPos;
    public float DropDistance = 1.0f;

    private bool isLocked = false;

    public GameObject puzzleCanvas;

    public static int correctDrops = 0;
    public static int totalObjects = 4;

    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
             puzzleCanvas.SetActive(false); // Закрываем Canvas
        }
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, ObjectDragToPos.transform.position);
        if (Distance < DropDistance)
        {
            isLocked = true;
            objectToDrag.transform.position = ObjectDragToPos.transform.position;

            correctDrops++; // Увеличиваем счетчик

            if (correctDrops >= totalObjects)
            {
                GameEventsManager.instance.miscEvents.PuzzleSolved();
                Debug.Log("Поздравляем! Головоломка решена!");
                puzzleCanvas.SetActive(false); // Закрываем Canvas
                CameraService.Instance.UnLock();
            }
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}
