using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Renderer visual;
    private static bool isTimeToShow = false;
    public static bool isTimeToHide = false;
    private float visability = 0f;

    void Start()
    {
        visual = GetComponent<Renderer>();
        visual.enabled = false;
        StartCoroutine(ExampleCoroutine(20));
    }

    private IEnumerator ExampleCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 0);
        visual.enabled = true;
        isTimeToShow = true;
    }


    void Update()
    {
        if (isTimeToShow)
        {            
            if (isTimeToHide)
            {
                var spriteRebderer = GetComponent<SpriteRenderer>();
                spriteRebderer.color = new Color(0, 0, 0, visability);
                visability -= 0.0007f;
                if (visability <= 0f)
                {
                    isTimeToShow = false;
                }
            }
            else
            {
                var spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(0, 0, 0, visability);
                visability += 0.002f;
                if (visability >= 1f)
                {
                    isTimeToHide = true;
                    spriteRenderer.color = new Color(0, 0, 0, 1);
                }
            }
        }

        if (StartButton.IsItLetsGo)
        {
            isTimeToShow = true;
            isTimeToHide = false;
            visability = 0f;
            StartButton.IsItLetsGo = false;
        }
    }
}
