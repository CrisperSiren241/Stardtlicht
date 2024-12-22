using UnityEngine;

public class OscillateRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.right;
    public float angleAmplitude = 30f;
    public float frequency = 1f;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float angleOffset = Mathf.Sin(Time.time * frequency) * angleAmplitude;
        Quaternion oscillationRotation = Quaternion.AngleAxis(angleOffset, rotationAxis);
        transform.rotation = initialRotation * oscillationRotation;
    }
}
