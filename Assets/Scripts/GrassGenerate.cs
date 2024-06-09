using UnityEngine;
using System.Collections;

public class GrassGenerate : MonoBehaviour
{
    public GameObject grassPrefab;
    public float spawnInterval = 1f;
    public float minXDistanceToRyan = 30f;
    public float maxDistanceToDelete = 40f;

    private float _nextSpawnTime;

    void Update()
    {
        if (Train.arrived && !GameManager.isItGameOver)
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + spawnInterval;

                GameObject ryan = GameObject.Find("Ryan");
                Vector3 ryanPosition = ryan.transform.position;

                float minY = GameManager.TopBorder + 2.5f;
                float maxY = GameManager.TopBorder + 4;
                float minBottomY = GameManager.BottomBorder - 2.5f;
                float maxBottomY = GameManager.BottomBorder - 4;

                float y;
                if (Random.Range(0, 2) == 0)
                {
                    y = Random.Range(minY, maxY);
                }
                else
                {
                    y = Random.Range(minBottomY, maxBottomY);
                }

                float x = ryanPosition.x + minXDistanceToRyan;

                GameObject grassClone = Instantiate(grassPrefab, new Vector3(x, y, 0), Quaternion.identity);

                StartCoroutine(DestroyGrassIfTooFar(grassClone));
            }
        }
    }

    private IEnumerator DestroyGrassIfTooFar(GameObject grassClone)
    {
        while (true)
        {
            Vector3 grassPosition = grassClone.transform.position;
            Vector3 ryanPosition = GameObject.Find("Ryan").transform.position;

            float distance = Vector3.Distance(grassPosition, ryanPosition);

            if (distance > maxDistanceToDelete)
            {
                Destroy(grassClone);
                break;
            }

            yield return null;
        }
    }
}