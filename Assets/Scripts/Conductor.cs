using UnityEngine;
using System.Collections;
using System.Linq;

public class Conductor : MonoBehaviour
{
    public GameObject target;
    public float MaxSpeed = 2;

    void Start() => GetComponent<Renderer>().enabled = false;

    void FixedUpdate()
    {
        if (!target)
            return;

        if (GetComponent<Renderer>().enabled && !GameManager.isItGameOver)
        {
            Vector3 direction = (target.transform.position - transform.position)
                .normalized;

            transform.Translate(direction * MaxSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ryan")
            GameManager.isItGameOver = true;

        if (collision.gameObject.name == "cake(Clone)" && !CakeScript.wasThrown)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
    }
}
