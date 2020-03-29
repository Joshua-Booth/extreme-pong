using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float topBounds = 8.5f;
    public float bottomBounds = -8.5f;
    public Vector2 startingPosition = new Vector3(-14.0f, 0f, 0f);

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = (Vector3)startingPosition;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        // Get the input
        float verticalInput;
        verticalInput = Input.GetAxis("Vertical");

        // Move the player
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * verticalInput);

        // Check the player's position is within the game boundaries
        if (transform.localPosition.y > topBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
        }

        if (transform.localPosition.y < bottomBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
        }
    }
}
