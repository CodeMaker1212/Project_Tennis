using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager 
{
    public static int MaxScores { get; set; } = 3;
    public static int PlayerScores { get; private set; }
    public static int EnemyScores { get; private set; }

    public static void AddScoreTo(string name)
    {
        switch (name)
        {
           case "Player":
                PlayerScores = Mathf.Clamp(PlayerScores + 1, 0, MaxScores);
                break;

            case "Enemy":
                EnemyScores = Mathf.Clamp(EnemyScores + 1, 0, MaxScores);
                break;
        }
    }
    public static void DeleteScoreFrom(string name)
    {
        switch (name)
        {
            case "Player":
                PlayerScores = Mathf.Clamp(PlayerScores - 1, 0, MaxScores);
                break;

            case "Enemy":
                EnemyScores = Mathf.Clamp(EnemyScores - 1, 0, MaxScores);
                break;
        }
    }
}
