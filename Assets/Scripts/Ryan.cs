using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Ryan : MonoBehaviour
{
    public float speed = 3f;
    private static bool isTimeToShowSkin = false;
    private Renderer visual;
    public Animator animator;
    public static string skin = "Ryan";
    public static int currentSkin = 0;
    public static string moveDirection = "";
    public float maxSpeed;

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
        isTimeToShowSkin = true;
    }

    void FixedUpdate()
    {
        if (isTimeToShowSkin)
        {
            if (skin == "Ken")
            {
                animator.SetFloat("SkinType", 1.5f);
            }
            else if (skin == "Ryan")
            {
                animator.SetFloat("SkinType", 0.5f);
            }
            else
            {
                animator.SetFloat("SkinType", 2.5f);
            }
        }
        if (GameManager.RyanMove)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newPosition = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce((Vector2)(mousePosition - transform.position)* speed);
            rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, maxSpeed);


            if (newPosition.x > mousePosition.x)
            {
                moveDirection = "left";
                transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (newPosition.x < mousePosition.x)
            {
                moveDirection = "right";
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
