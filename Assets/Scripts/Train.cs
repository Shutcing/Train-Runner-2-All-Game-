using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public const float trainLength = 29.162f;
    private static float speed = 0.04f;
    public Sprite NewSprite;
    private Renderer visual;
    public static bool arrived;
    private GameObject ryan;

    void Start()
    {
        visual = GetComponent<Renderer>();
        visual.enabled = false;
        ryan = GameObject.Find("Ryan");
    }

    // Update is called once per frame
    void Update()
    {
        if (visual.enabled)
        {
            if (transform.position.x < 0.7)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
            else if (!arrived)
            {
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.sprite = NewSprite;
                arrived = true;
            }

            if (arrived)
            {
                var trainNose = GameObject.Find("TrainNose");
                trainNose.transform.position = new Vector3(transform.position.x + 20, transform.position.y, 0);
                var position = transform.position;
                position.x = (int)(ryan.transform.position.x / trainLength) * trainLength;
                transform.position = position;
            }
        }
    }
}