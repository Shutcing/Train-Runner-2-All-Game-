using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePrefabs : MonoBehaviour
{
    public float CursorOffset;
    public float objStep;
    public float firstObjOffset = 0;
    public GameObject Prefab;

    private Queue<GameObject> objs = new();
    private float nextObjXCoord;

    void Start()
    {
        nextObjXCoord = -firstObjOffset;
    }

    void Update()
    {
        if (GameManager.RyanMove)
        {
            var generationCursor = GameManager.ryan.transform.position.x - CursorOffset;
            var deletetionCursor = GameManager.ryan.transform.position.x + CursorOffset;

            if (generationCursor < nextObjXCoord)
            {
                var seatPosition = new Vector3(nextObjXCoord, Random.Range(GameManager.BottomBorder, GameManager.TopBorder));
                objs.Enqueue(Instantiate(Prefab, seatPosition, Quaternion.identity));

                nextObjXCoord -= objStep;
            }

            if (objs.TryPeek(out var oldestObj) && deletetionCursor < oldestObj.transform.position.x)
            {
                Destroy(oldestObj);
                objs.Dequeue();
                if (objStep > 0)
                    objStep -= 0.01f;
            }
        }
    }
}
