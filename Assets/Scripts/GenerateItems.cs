using System.Collections;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public GameObject newItem; // ������ �������� ���������
    private float bottomBorder; // ������ ������� ���������
    private float topBorder; // ������� ������� ���������
    public float spawnInterval = 15; // �������� ����� �����������
    public float despawnDistance = 25; // ����������, �� ������� ������� ���������

    private Transform ryan; // ������ �� ������ Ryan
    private float nextSpawnTime; // ����� ��������� ���������

    private void Start()
    {
        ryan = GameObject.Find("Ryan").transform;
        nextSpawnTime = Time.time + spawnInterval;
        bottomBorder = GameManager.BottomBorder; // ������ ������� ���������
        topBorder = GameManager.TopBorder;
        newItem.transform.localScale = new Vector3(0.332856f, 0.332856f, 0);
    }

    private void Update()
    {
        if (Train.arrived && !GameManager.isItGameOver)
        {
            // ��������� ����� ��������� ���������
            var bat = GameObject.Find("bat(Clone)");
            var cake = GameObject.Find("cake(Clone)");
            if (Time.time >= nextSpawnTime && (bat == null && newItem.name == "bat") || (cake == null && newItem.name == "cake"))
            {
                // ���������� ��������� �������
                GameObject item = Instantiate(newItem, new Vector3(ryan.position.x - 20, Random.Range(bottomBorder, topBorder), 0), Quaternion.identity);
                item.transform.localScale = new Vector3(0.332856f, 0.332856f, 0);
                if (newItem.name == "cake")
                {
                    CakeScript.wasThrown = false;
                }

                // ��������� ����� ��������� ���������
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
     }
}