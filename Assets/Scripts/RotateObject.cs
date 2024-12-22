using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 50f;
    public float radius = 2f;
    private float angle = 0f;

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        float angleRad = angle * Mathf.Deg2Rad;
        float x = pivotPoint.position.x + Mathf.Cos(angleRad) * radius;
        float z = pivotPoint.position.z + Mathf.Sin(angleRad) * radius;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}