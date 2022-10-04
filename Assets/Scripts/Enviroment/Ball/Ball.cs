using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float minStartSpeed,maxStartSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(minStartSpeed,maxStartSpeed),Random.Range(minStartSpeed,maxStartSpeed)); //Adds a random speed to the ball
    }
}
