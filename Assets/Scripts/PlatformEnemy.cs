using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemy : MonoBehaviour
{
    public Transform[] points;   // Массив точек для движения платформы
    public float speed = 2f;     // Скорость движения платформы
    private int currentPointIndex = 0;  // Индекс текущей целевой точки

    // Start is called before the first frame update
    void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;  // Устанавливаем начальную позицию платформы в первую точку
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length == 0) return; // Проверка на наличие точек

        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, points[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;  // Зацикливаемся на массиве точек
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         other.transform.SetParent(transform); // Прикрепляем игрока к платформе
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         other.transform.SetParent(null); // Отвязываем игрока от платформы
    //     }
    // }
}
