using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    private float scaleAmount = 0.4f;
    private float scaleSpeed = 1.5f;
    private Renderer visual;

    void Start()
    {
        visual = GetComponent<Renderer>();
        visual.enabled = false;
        StartCoroutine(ExampleCoroutine(22));
    }

    IEnumerator ExampleCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        visual.enabled = true;
    }

    void Swaying()
    {
        float currentScaleY = transform.localScale.y;
        float targetScaleY = currentScaleY + Mathf.PingPong(Time.time * scaleSpeed, scaleAmount) - scaleAmount / 2;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(currentScaleY, targetScaleY, Time.deltaTime * scaleSpeed), transform.localScale.z);
    }

    void Update()
    {
        string objectName = gameObject.name;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (objectName == "Ryan")
        {
            Swaying();
        }
        else if (Math.Abs(transform.position.x - mousePosition.x) < 0.5 && Math.Abs(transform.position.y - mousePosition.y) < 0.5 && objectName != "back" && objectName != "Light")
        {
            Swaying();
        }
        else if (objectName == "ArrowLeft" || objectName == "ArrowRight")
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.2354f, transform.localScale.z);
        }

    }
}
