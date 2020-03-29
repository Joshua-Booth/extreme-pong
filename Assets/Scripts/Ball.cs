using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 12.0f;
    public Vector2 ballDirection = Vector2.right;
    Rigidbody2D rigidBody;

    private UI_Manager uiManager;
    private Game _game;
    private Player _player;
    private Computer _computer;

    private int playerScore;
    private int computerScore;
    public Vector2 startingPosition = new Vector3(0f, 0f, 0f);
    float dificultyMultiplier;
    private float timeSinceLastHit;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip goalAudioClip;
    public AudioClip wallBounceAudioClip;

    private Animator ballAnimation;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _game = GameObject.Find("Game").GetComponent<Game>();
        _player = _game.player;
        _computer = _game.computer;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(moveSpeed, -3f);
        ballDirection = Vector2.right;
        audioSource = GetComponent<AudioSource>();
        ballAnimation = GetComponent<Animator>();
        ballAnimation.SetTrigger("ballCollison");
    }

    // Update is called once per frame
    void Update()
    {
        // Change the ball direction if the ball gets stuck
        if (Time.time - timeSinceLastHit > 30)
        {
            Direction();
        }
    }

    public void Direction()
    {
        timeSinceLastHit = Time.time;

        int direction = 0;
        int randomHeight;

        // Ball height variation to help the player against the speed of the computer
        if (playerScore < 5)
        {
            randomHeight = Random.Range(3, 6);
        }
        else if (playerScore < 10)
        {
            randomHeight = Random.Range(6, 10);
        }
        else
        {
            randomHeight = Random.Range(6, 15);
        }

        // Potentially have the ball randomly spawn towards or away from the player
        // This first statement is never run as the direction is always towards the computer
        if (direction == 1)
        {
            rigidBody.velocity = new Vector2(moveSpeed, randomHeight);
            ballDirection = Vector2.right;
        }
        else
        {
            rigidBody.velocity = new Vector2(-moveSpeed, randomHeight);
            ballDirection = Vector2.left;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Computer scores against the player
        if (collider.tag == "player goal")
        {
            // Play audio
            audioSource.clip = goalAudioClip;
            audioSource.Play();
            // Score is added
            computerScore++;
            uiManager.UpdateComputerScore(computerScore);
            // Enemey score 'animation'
            StartCoroutine(uiManager.enemyScoreFlicker(playerScore));
            // Position Reset
            _computer.transform.position = _computer.startingPosition;
        }
        // Player scores against the computer
        else if (collider.tag == "computer goal")
        {
            // Play audio
            audioSource.clip = goalAudioClip;
            audioSource.Play();
            // Score is added
            playerScore++;
            uiManager.UpdatePlayerScore(playerScore);
            // Check to see if level can be changed
            _game.ChangeLevel(playerScore);
            // Enemey score 'animation'
            StartCoroutine(uiManager.playerScoreFlicker(playerScore));

            // Speed is changed based on player's points
            if (moveSpeed < 25)
            {
                moveSpeed++;
            }
        }
        else if (collider.tag == "player")
        {
            // Play animation
            ballAnimation.SetTrigger("ballCollison");
            // Play audio
            audioSource.clip = audioClip;
            audioSource.Play();
            // Change ball direction
            rigidBody.velocity = new Vector2(moveSpeed, Random.Range(-10, 10 * _game.dificultyConstant));
            ballDirection = Vector2.right;
            return;
        }
        // Restart the ball infront of the computer
        transform.position = new Vector3(10f, _computer.transform.position.y, transform.position.z);
        Direction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play sound based on what oject was collided with
        if (collision.collider.tag == "computer")
        {
            ballAnimation.SetTrigger("ballCollison");
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (collision.collider.tag == "wall")
        {
            ballAnimation.SetTrigger("ballCollison");
            audioSource.clip = wallBounceAudioClip;
            audioSource.Play();
        }
    }
}
