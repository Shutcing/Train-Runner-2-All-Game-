using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class Grass : MonoBehaviour
{
    private Renderer visual;

    void Update()
    {
        visual = GetComponent<Renderer>();
        if (Train.arrived && !GameManager.isItGameOver)
        {
            visual.enabled = true;
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        }
    }
}
