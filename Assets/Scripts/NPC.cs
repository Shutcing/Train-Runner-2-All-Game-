using System.Linq;
using UnityEngine;
using System;

public class NPC : MonoBehaviour
{
    private Renderer visual;

    private Rigidbody2D rb2d;

    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minTime = 1.5f;
    public float maxTime = 3f;

    private float nextMovementTime;
    private Vector2 movementDirection;

    void Start()
    {
        visual = GetComponent<Renderer>();
        rb2d = GetComponent<Rigidbody2D>();
        nextMovementTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ѕровер€ем, столкнулись ли мы с другим персонажем
        if (collision.gameObject.name == "bat(Clone)" && Math.Abs(collision.gameObject.transform.localEulerAngles.z) > 1)
        {
            if (collision.transform.position.x < transform.position.x)
            {
                transform.position -= collision.transform.position.normalized * 2;
            }
            else
            {
                transform.position += collision.transform.position.normalized * 2;
            }
        }
    }

    void Update()
    {
        if (!visual.enabled || Time.time < nextMovementTime)
            return;
        movementDirection = UnityEngine.Random.insideUnitCircle.normalized;
        var movementDelay = UnityEngine.Random.Range(minTime, maxTime);
        nextMovementTime = Time.time + movementDelay;
        rb2d.velocity = movementDirection * UnityEngine.Random.Range(minSpeed, maxSpeed);
    }
}