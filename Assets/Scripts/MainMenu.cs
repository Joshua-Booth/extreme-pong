using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Loads Game Scene
    public void LoadGame()
    {
        SceneManager.LoadScene("Extreme Pong");
    }
}
