using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static UnityEditor.Progress;

public class batScript : MonoBehaviour
{
    private GameObject handLeft;
    private GameObject handRight;
    public static bool TakeBat = false;
    private bool Punch = false;
    private int angle;
    private string hand = "";

    private Renderer visual;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new[] { "Ryan", "Hand1", "Hand2" }.Contains(collision.gameObject.name) && TakeBat)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
    }

    void Start()
    {
        visual = GetComponent<Renderer>();
        handLeft = GameObject.Find("Hand1");
        handRight = GameObject.Find("Hand2");
    }

    private bool AreObjectsNearby(GameObject obj1, GameObject obj2)
    {
        return Math.Abs(obj1.transform.position.x - obj2.transform.position.x) < 0.5f
            && Math.Abs(obj1.transform.position.y - obj2.transform.position.y) < 0.5f;
    }

    private void StayInHand(Vector2 mousePosition)
    {
        if (transform.position.x >= mousePosition.x && TakeBat)
        {
            transform.position = new Vector3(handLeft.transform.position.x, handLeft.transform.position.y, 0);
            hand = "left";
        }
        else if (transform.position.x < mousePosition.x && TakeBat)
        {
            transform.position = new Vector3(handRight.transform.position.x, handRight.transform.position.y, 0);
            hand = "right";
        }
    }

    private void OnPunch()
    {
        if (hand == "left")
        {
            transform.Rotate(0, 0, 6, Space.Self);
        }
        else
        {
            transform.Rotate(0, 0, -6, Space.Self);
        }
        angle += 6;
        if (angle == 360)
        {
            Punch = false;
            angle = 0;
        }
    }


    void Update()
    {
        visual.enabled = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (AreObjectsNearby(gameObject, handLeft) || AreObjectsNearby(gameObject, handRight))
        {
            if (!GameManager.TakeSomething)
                TakeBat = true;
        }

        StayInHand(mousePosition);


        if (Input.GetKeyDown(KeyCode.Space) && TakeBat)
        {
            Punch = true;
        }

        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && TakeBat)
        {
            TakeBat = false;
            transform.position += new Vector3(mousePosition.normalized.x * 2, mousePosition.normalized.y * 2, 0);
        }

        if (Punch)
        {
            OnPunch();
        }

        if (Math.Abs(transform.position.x - handLeft.transform.position.x) > 21)
            Destroy(gameObject);
    }
}
