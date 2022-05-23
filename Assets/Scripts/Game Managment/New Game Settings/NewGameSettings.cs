using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using environments = ProjectEnums.Enums.Environments;
using difficulty = ProjectEnums.Enums.DifficultyLevel;

public class NewGameSettings : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("Game");
    public void BackToMainMenu() => SceneManager.LoadScene("Main_Menu");
    protected void ChooseEnvironment(environments chosenEnvironment) => MainManager.Instance.chosenCourt = chosenEnvironment;
    protected void ChooseDifficulty(difficulty chosenDifficulty) => MainManager.Instance.chosenDifficulty = chosenDifficulty;
    protected void ChooseMaxScores(int chosenMaxScores) => MainManager.Instance.chosenMaxScores = chosenMaxScores;
    public void EnableTutorial()
    {
        if (MainManager.Instance.tutorialEnabled == false)
        MainManager.Instance.tutorialEnabled = true;
    }
   
 
}
