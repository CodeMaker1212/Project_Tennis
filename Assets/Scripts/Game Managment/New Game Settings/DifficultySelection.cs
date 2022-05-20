using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using difficulty = ProjectEnums.Enums.DifficultyLevel;

public class DifficultySelection : NewGameSettings
{
    private Text _difficultyName;
    private Text _difficultyDescription;

    private difficulty _currentDifficulty;
  
    public void SetEasyDifficulty()
    {
        _difficultyName.text = difficulty.EASY.ToString();
        _difficultyDescription.text = "� ���������� ������ ��������, �������� � ���� ������. ����� ��� ������ �� ����� ������� �����";
        _currentDifficulty = difficulty.EASY;

        ChooseDifficulty(_currentDifficulty);
    }
    public void SetHardDifficulty()
    {
        _difficultyName.text = difficulty.HARD.ToString();
        _difficultyDescription.text = "��������� �� �������� ��� �� ����, ������ ������������, ����� ������������� � ������ ����";
        _currentDifficulty = difficulty.HARD;

        ChooseDifficulty(_currentDifficulty);
    }

    private void Awake()
    {
        _difficultyName = GameObject.Find("Difficulty_Name").GetComponent<Text>();
        _difficultyDescription = GameObject.Find("Difficulty_Description").GetComponent<Text>();

        SetEasyDifficulty();

    }
}
