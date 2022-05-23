using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using environments = ProjectEnums.Enums.Environments;
using difficulty = ProjectEnums.Enums.DifficultyLevel;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public environments chosenCourt;
    public difficulty chosenDifficulty;
    public int chosenMaxScores;
    public bool tutorialEnabled = false;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
