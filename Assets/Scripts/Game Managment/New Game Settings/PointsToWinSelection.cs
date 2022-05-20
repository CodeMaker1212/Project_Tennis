using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsToWinSelection : NewGameSettings
{
    private const int _minPoints = 3;
    private const int _maxPoints = 10;

    private Text _points;
    private int _currentNumber;


    public void IncreasePoints()
    {    
        _currentNumber = Mathf.Clamp((_currentNumber + 1), _minPoints, _maxPoints);
        _points.text = _currentNumber.ToString();
        ChooseMaxScores(_currentNumber);
    }
    public void ReducePoints()
    {
        _currentNumber = Mathf.Clamp((_currentNumber - 1), _minPoints, _maxPoints);
        _points.text = _currentNumber.ToString();
        ChooseMaxScores(_currentNumber);
    }


    private void Awake()
    {
        _points = GameObject.Find("Points_Number").GetComponent<Text>();
        _currentNumber = _minPoints;
        ChooseMaxScores(_currentNumber);

    }
}
