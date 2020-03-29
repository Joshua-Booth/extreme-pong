using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float topBounds = 9.5f;
    public float bottomBounds = -9.5f;
    public Vector2 startingPosition = new Vector3(14.0f, 0f, 0f);

    Rigidbody2D rigidBody;
    public GameObject ball;
    private Vector2 ballPos;
    public float maximumSpeed = 7.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = (Vector3)startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // Check the ball exists
        if (!ball)
        {
            ball = GameObject.FindGameObjectWithTag("ball");
        }

        // Find the ball
        if (ball.GetComponent<Ball>().ballDirection == Vector2.right || 
            ball.GetComponent<Ball>().ballDirection == Vector2.left)
        {
            ballPos = ball.transform.localPosition;

            // Move to the ball
            if (transform.localPosition.y > bottomBounds && ballPos.y < transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
            }

            if (transform.localPosition.y < topBounds && ballPos.y > transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<Ball>().Direction();
        collider.transform.position = new Vector2(0f, 0f);
    }
}
