using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class Leg_script : MonoBehaviour
{
    public GameObject obj;
    private static float speed = 20f;
    private Renderer visual;
    private int tact = 0;


    void Start()
    {
        visual = GetComponent<Renderer>();
        visual.enabled = false;
    }

    IEnumerator ExampleCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        visual.enabled = true;
    }

    private void MakeTurn(int direction)
    {
        if (tact == 12)
        {
            ExampleCoroutine(4);
        }
        if (tact % 12 == 0)
            transform.Rotate(0, 0, direction * 900 * Time.deltaTime, Space.Self);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (obj.GetComponent<Renderer>().enabled && GameManager.RyanMove)
        {
            visual.enabled = true;
            tact +=4;
            Vector2 objToFollow = obj.transform.position;
            string objectName = gameObject.name;

            objToFollow.y -= 1f;
            if (objectName.Contains("Leg1"))
            {
                objToFollow.x -= 0.2f;
            }
            else
            {
                objToFollow.x += 0.2f;
            }

            transform.position = Vector2.MoveTowards(transform.position, objToFollow, speed * Time.deltaTime);
            if (objectName.Contains("Leg1"))
            {
                if (Math.Abs(obj.transform.position.x) < Math.Abs(objToFollow.x))
                {
                    if (tact % 4 == 0)
                        transform.Rotate(0, 0, 500 * Time.deltaTime, Space.Self);
                }
                else if (Math.Abs(obj.transform.position.x) > Math.Abs(objToFollow.x))
                {
                    if (tact % 4 == 0)
                        transform.Rotate(0, 0, -500 * Time.deltaTime, Space.Self);
                }
            }
            else
            {
                if (Math.Abs(obj.transform.position.x) > Math.Abs(objToFollow.x))
                {
                    MakeTurn(1);
                }
                else if (Math.Abs(obj.transform.position.x) < Math.Abs(objToFollow.x))
                {
                    MakeTurn(-1);
                }
            }
        }
    }
}
