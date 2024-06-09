using System.Collections;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public GameObject newItem; // Массив префабов предметов
    private float bottomBorder; // Нижняя граница генерации
    private float topBorder; // Верхняя граница генерации
    public float spawnInterval = 15; // Интервал между генерациями
    public float despawnDistance = 25; // Расстояние, на котором предмет удаляется

    private Transform ryan; // Ссылка на объект Ryan
    private float nextSpawnTime; // Время следующей генерации

    private void Start()
    {
        ryan = GameObject.Find("Ryan").transform;
        nextSpawnTime = Time.time + spawnInterval;
        bottomBorder = GameManager.BottomBorder; // Нижняя граница генерации
        topBorder = GameManager.TopBorder;
        newItem.transform.localScale = new Vector3(0.332856f, 0.332856f, 0);
    }

    private void Update()
    {
        if (Train.arrived && !GameManager.isItGameOver)
        {
            // Проверяем время следующей генерации
            var bat = GameObject.Find("bat(Clone)");
            var cake = GameObject.Find("cake(Clone)");
            if (Time.time >= nextSpawnTime && (bat == null && newItem.name == "bat") || (cake == null && newItem.name == "cake"))
            {
                // Генерируем случайный предмет
                GameObject item = Instantiate(newItem, new Vector3(ryan.position.x - 20, Random.Range(bottomBorder, topBorder), 0), Quaternion.identity);
                item.transform.localScale = new Vector3(0.332856f, 0.332856f, 0);
                if (newItem.name == "cake")
                {
                    CakeScript.wasThrown = false;
                }

                // Обновляем время следующей генерации
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
     }
}