using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{

    public GeneratePrefabs generator;
    private int level = 0;
    public GameObject newPrefab;

    void Update()
    {
        if (level == 0 && Time.time > 100)
        {
            generator.objStep = 20;
            generator.Prefab = newPrefab;
            level = 1;
        }
        else if (level == 1 && Time.time > 150)
        {
            generator.objStep = 10;
            level = 2;
        }
    }
}
