using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartButton : MonoBehaviour
{
    public static bool IsItLetsGo = false;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Math.Abs(transform.position.x - mousePosition.x) < 0.5 && Math.Abs(transform.position.y - mousePosition.y) < 0.5)
        {
            if (Input.GetMouseButtonDown(0))
                IsItLetsGo = true;
        }
    }
}