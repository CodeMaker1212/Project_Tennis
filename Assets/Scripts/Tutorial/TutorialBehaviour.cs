using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorialScreens;
    private int _currentScreen;


    public void SwitchTutorialScreens(int dropdownItemValue)
    {
        if (_currentScreen != dropdownItemValue)
            _tutorialScreens[_currentScreen].SetActive(false);

        _tutorialScreens[dropdownItemValue].SetActive(true);
        _currentScreen = dropdownItemValue;     
    }
    public void BackToMainMenu() => SceneManager.LoadScene("Main_Menu");
}
