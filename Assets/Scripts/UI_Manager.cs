using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text playerScoreText;
    public Text computerScoreText;
    public Text escText;

    public int playerScore = 0;
    public int computerScore;

    public int HighScore = 0;
    public Text HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = playerScore.ToString();
        computerScoreText.text = playerScore.ToString();
        HighScore = PlayerPrefs.GetInt("HighScore", HighScore);
        HighScoreText.text = "High Score: " + HighScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Escape text flickers
    public IEnumerator escTextFlicker()
    {
        while (true)
        {
            escText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            escText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    //When the enemy scores there score flickers a different color
    public IEnumerator enemyScoreFlicker(int computerScore)
    {
        //Score text changes red for 0.5 sec then back to default color based on the current level color scheme
        if (computerScore <= 4)
        {
            computerScoreText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            computerScoreText.color = Color.white;
        }
        else if (computerScore >= 5)
        {
            computerScoreText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            computerScoreText.color = Color.grey;
        }
        else if (computerScore >= 25)
        {
            computerScoreText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            computerScoreText.color = Color.white;
        }
    }

    //When the enemy scores there score flickers a different color
    public IEnumerator playerScoreFlicker(int playerScore)
    {
        //Score text changes green for 0.5 sec then back to default color based on the current level color scheme
        if (playerScore <= 4)
        {
            playerScoreText.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            playerScoreText.color = Color.white;
        }
        else if (playerScore >= 5)
        {
            playerScoreText.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            playerScoreText.color = Color.grey;
        }
        else if (playerScore >= 25)
        {
            playerScoreText.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            playerScoreText.color = Color.white;
        }
    }

    //Players score is updated and reflected in game
    public void UpdatePlayerScore(int score)
    {
        playerScore = score;
        playerScoreText.text = playerScore.ToString();
        
        //Checks if the current score is higher than the high score
        CheckForHighScore(playerScore);
    }

    //Updates the computer score and is reflected in game
    public void UpdateComputerScore(int score)
    {
        computerScore = score;
        computerScoreText.text = computerScore.ToString();
    }

    public void CheckForHighScore(int score)
    {
        // Check and save the highest score
        if (score > HighScore)
        {
            HighScore = score;
            HighScoreText.text = "High Score: " + score;

            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
    }
}
