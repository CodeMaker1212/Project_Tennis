using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("New_Game_Settings");
    public void StartTutorial() => SceneManager.LoadScene("Tutorial");
    public void Exit() => Application.Quit();
}

    
