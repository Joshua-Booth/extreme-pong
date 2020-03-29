using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Ball _ball;

    public Player player { get; private set; }
    private Player _player;
    public GameObject playerPrefab;

    public Computer computer { get; private set; }
    private Computer _computer;
    public GameObject computerPrefab;

    public float startTime;
    public int dificultyConstant = 2;
    private UI_Manager uiManager;
    float dificultyMultiplier;

    private bool isGameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        _ball = GameObject.Find("ball1").GetComponent<Ball>();
        
        // Create temporary objects to get starting positions of paddles
        GameObject tempObject = new GameObject();
        _computer = tempObject.AddComponent<Computer>();
        _player = tempObject.AddComponent<Player>();

        _computer = Instantiate(computerPrefab, _computer.startingPosition, Quaternion.identity).GetComponent<Computer>();
        computer = _computer;
        _player = Instantiate(playerPrefab, _player.startingPosition, Quaternion.identity).GetComponent<Player>();
        player = _player;

        Destroy(tempObject);

        uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        SpawnBall();

        uiManager.StartCoroutine(uiManager.escTextFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        // Limit the max speed
        if (_computer.moveSpeed > _computer.maximumSpeed)
            return;
        // Computer will gain speed as the player gains points
        _computer.moveSpeed = (uiManager.playerScore + 1) * 1.05f * dificultyConstant - 0.5f;

        //Escape key quits game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //press p to pause/continue game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGameActive == true)
            {
                pauseGame();
            }
            else if (isGameActive == false)
            {
                continueGame();
            }
        }
    }

    //Pause game
    private void pauseGame()
    {
        Time.timeScale = 0;
        isGameActive = false;
    }

    //Continue game
    private void continueGame()
    {
        Time.timeScale = 1;
        isGameActive = true;
    }

    public void ChangeLevel(int score)
    {
        // Change the level based on the points scored by the player

        // Player gets faster
        // Background changes, computer, ball, player and score text colour changes

        SpriteRenderer background = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        if (score >= 5 && score < 15)
        {
            _player.moveSpeed += 4;

            background.sprite = Resources.Load<Sprite>("Backgrounds/level1");
            background.transform.localScale = new Vector3(0.76f, 0.76f, 0f);

            _player.GetComponent<SpriteRenderer>().color = Color.gray;
            _computer.GetComponent<SpriteRenderer>().color = Color.gray;
            _ball.GetComponent<SpriteRenderer>().color = Color.gray;

            uiManager.playerScoreText.color = Color.gray;
            uiManager.computerScoreText.color = Color.gray;
            uiManager.HighScoreText.color = Color.gray;
        }
        else if (score >= 15 && score < 25)
        {
            _player.moveSpeed += 2;

            background.sprite = Resources.Load<Sprite>("Backgrounds/level2");
            background.transform.localScale = new Vector3(0.76f, 0.76f, 0f);
            dificultyConstant = 3;
        }
        else if (score >= 25)
        {
            _player.moveSpeed += 2;

            background.sprite = Resources.Load<Sprite>("Backgrounds/level3");
            background.transform.localScale = new Vector3(1, 1, 1);

            _player.GetComponent<SpriteRenderer>().color = Color.white;
            _computer.GetComponent<SpriteRenderer>().color = Color.white;
            _ball.GetComponent<SpriteRenderer>().color = Color.green;

            uiManager.playerScoreText.color = Color.white;
            uiManager.computerScoreText.color = Color.white;
            uiManager.HighScoreText.color = Color.white;
            dificultyConstant = 4;
        }
    }

    void SpawnBall()
    {
        _ball.transform.localPosition = new Vector3(12, 0, -2);
    }

}
