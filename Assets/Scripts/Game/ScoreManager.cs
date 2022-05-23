using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;

public class ScoreManager: MonoBehaviour
{
    public static int MaxScores { get; private set; }
    public static int PlayerScores { get; private set; }
    public static int EnemyScores { get; private set; }


    private void SetMaxScores(int chosenMaxScores) => MaxScores = chosenMaxScores;
    public static void AddScoreTo(participants name)
    {
        switch (name)
        {
           case participants.Player:
                PlayerScores = Mathf.Clamp(PlayerScores + 1, 0, MaxScores);
                break;

            case participants.Enemy:
                EnemyScores = Mathf.Clamp(EnemyScores + 1, 0, MaxScores);
                break;
        }
    }
    public static void DeleteScoreFrom(participants name)
    {
        switch (name)
        {
            case participants.Player:
                PlayerScores = Mathf.Clamp(PlayerScores - 1, 0, MaxScores);
                break;

            case participants.Enemy:
                EnemyScores = Mathf.Clamp(EnemyScores - 1, 0, MaxScores);
                break;
        }
    }
    public static void ResetScores()
    {
        PlayerScores = 0;
        EnemyScores = 0;
    }

    private void Awake()
    {
        SetMaxScores(MainManager.Instance.chosenMaxScores);
    }

 
}
