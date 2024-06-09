using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Rendering;

public class CakeScript : MonoBehaviour
{
    private Renderer visual;
    private GameObject handLeft;
    private GameObject handRight;
    public static bool TakeCake = false;
    private string hand = "";
    public static float power;
    private bool WasPushed = false;
    private int ShootLength = 0;
    public static bool wasThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        handLeft = GameObject.Find("Hand1");
        handRight = GameObject.Find("Hand2");
        visual = GetComponent<Renderer>();
    }

    void UpdatePowerCapsule()
    {
        var capsule = GameObject.Find("Capsule");
        capsule.transform.localScale = new Vector3(power, 0.1f, 0);
        capsule.GetComponent<Renderer>().enabled = true;
        var ryan = GameObject.Find("Ryan").transform.position;
        capsule.transform.localPosition = new Vector3(ryan.x, ryan.y + 2, 0);
    }

    private void ChangeSkin()
    {
        if (wasThrown && ShootLength <= 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            var newSprite = Resources.Load<Sprite>("Sprites/CakePuddle");
            spriteRenderer.sprite = newSprite;
            if (transform.localScale.x < 0.6f)
                transform.localScale = transform.localScale * 2;
        }
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            var newSprite = Resources.Load<Sprite>("Sprites/Group 453");
            spriteRenderer.sprite = newSprite;
        }
    }

    private void StayInHand(Vector2 mousePosition)
    {
        if (transform.position.x > mousePosition.x && TakeCake)
        {
            transform.position = new Vector3(handLeft.transform.position.x + 2, handLeft.transform.position.y, 0);
            hand = "left";
        }
        else if (transform.position.x < mousePosition.x && TakeCake)
        {
            transform.position = new Vector3(handRight.transform.position.x - 2, handRight.transform.position.y, 0);
            hand = "right";
        }
    }

    // Update is called once per frame
    void Update()
    {
        visual.enabled = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (((Math.Abs(transform.position.x - handLeft.transform.position.x) < 0.5f && Math.Abs(transform.position.y - handLeft.transform.position.y) < 0.5f) ||
            (Math.Abs(transform.position.x - handRight.transform.position.x) < 0.5f && Math.Abs(transform.position.y - handRight.transform.position.y) < 0.5f)) &&
            GetComponent<SpriteRenderer>().sprite != Resources.Load<Sprite>("Sprites/CakePuddle"))
        {
            if (!GameManager.TakeSomething && ShootLength == 0)
                TakeCake = true;
        }

        StayInHand(mousePosition);

        if (Input.GetKey(KeyCode.Space) && TakeCake)
        {
            power += 0.01f;
            power = power < 2 ? power : 2;
            WasPushed = true;

        }
        else
        {   
            if (WasPushed)
            {
                WasPushed = false;
                TakeCake = false;
                ShootLength = (int)(10 * power);
                wasThrown = true;
            }
            power = 0;
        }

        UpdatePowerCapsule();

        if (ShootLength > 0)
        {
            if (Ryan.moveDirection == "left")
                transform.position += new Vector3 (-Math.Abs(mousePosition.normalized.x / 2), mousePosition.normalized.y / 2, 0);
            if (Ryan.moveDirection == "right")
                transform.position += new Vector3(Math.Abs(mousePosition.normalized.x / 2), mousePosition.normalized.y / 2, 0);
            ShootLength--;
        }

        ChangeSkin();

        if (Math.Abs(transform.position.x - handLeft.transform.position.x) > 21)
            Destroy(gameObject);
    }
}
