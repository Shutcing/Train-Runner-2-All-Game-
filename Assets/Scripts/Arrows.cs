using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrows : MonoBehaviour
{
    public List<string> skins = new List<string> { "Ryan", "Ken", "LaLaLand" };
    public static bool SkinIsChange = false;

    public void OnButtonClick(string direction)
    {
        SkinIsChange = true;
        skins.Add("LaLaLand");

        if (direction == "left")
        {
            ChangeSkinLeft();
        }
        else if (direction == "right")
        {
            ChangeSkinRight();
        }
    }

    void ChangeSkinLeft()
    {
        if (Ryan.currentSkin > 0)
        {
            Ryan.currentSkin = (Ryan.currentSkin - 1) % skins.Count;
            Ryan.skin = skins[Ryan.currentSkin];
        }
    }

    void ChangeSkinRight()
    {
        Ryan.currentSkin = (Ryan.currentSkin + 1) % skins.Count;
        Ryan.skin = skins[Ryan.currentSkin];
    }

    void Update()
    {
        string objectName = gameObject.name;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Math.Abs(transform.position.x - mousePosition.x) < 0.5 && Math.Abs(transform.position.y - mousePosition.y) < 0.5 && Input.GetMouseButtonDown(0) && objectName == "ArrowLeft")
        {
            OnButtonClick("left");
        }
        else if (Math.Abs(transform.position.x - mousePosition.x) < 0.5 && Math.Abs(transform.position.y - mousePosition.y) < 0.5 && Input.GetMouseButtonDown(0) && objectName == "ArrowRight")
        {
            OnButtonClick("right");
        }
    }
}