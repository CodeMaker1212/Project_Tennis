using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;

public class BallBehaviour : Ball
{
    public event DoubleTouchEvent DoubleTouch;

    private void Start()
    {
        InitializeReferences();
    }

    private void Update()
    {

        if (CourtTouchCount >= 2)
             DoubleTouch();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Player":
                ResetTouchCount();
                BounceOffPlayer();
                GameBehaviour.LastTouched = participants.Player;
                break;



            case "Enemy":
                ResetTouchCount();
                BounceOffEnemy();
                GameBehaviour.LastTouched = participants.Enemy;
                break;



            case "net":
                BounceOffNet();

                if (_gameBehaviour.RoundHasBegan == true)
                {
                    if (GameBehaviour.LastTouched == participants.Player)
                    {
                        GameBehaviour.NextRoundBeginner = participants.Enemy;
                        ScoreManager.DeleteScoreFrom(participants.Player);
                        _gameBehaviour.ShowTouchingNetText(participants.Player);
                        
                    }
                    else if (GameBehaviour.LastTouched == participants.Enemy)
                    {
                        GameBehaviour.NextRoundBeginner = participants.Player;
                        ScoreManager.DeleteScoreFrom(participants.Enemy);
                        _gameBehaviour.ShowTouchingNetText(participants.Enemy);
                       
                    }                 
                }
                break;





            case "Enemy_Border":
                GameBehaviour.NextRoundBeginner = participants.Player;
                ScoreManager.AddScoreTo(participants.Player);
                _gameBehaviour.ShowWhoGetsPointText(participants.Player);
                 break;



            case "Player_Border":
                GameBehaviour.NextRoundBeginner = participants.Enemy;
                ScoreManager.AddScoreTo(participants.Enemy);
                _gameBehaviour.ShowWhoGetsPointText(participants.Enemy);
                break;



            case "Out_Borders":

                if(_gameBehaviour.RoundHasBegan == true)
                {

                    if (GameBehaviour.LastTouched == participants.Player)
                    {
                        GameBehaviour.NextRoundBeginner = participants.Enemy;
                        ScoreManager.DeleteScoreFrom(participants.Player);
                        _gameBehaviour.ShowOutText(participants.Player);
                       
                    }
                    else if (GameBehaviour.LastTouched == participants.Enemy)
                    {
                        GameBehaviour.NextRoundBeginner = participants.Player;
                        ScoreManager.DeleteScoreFrom(participants.Enemy);
                        _gameBehaviour.ShowOutText(participants.Enemy);
                       
                    }
                }
                break;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
            case "floor":
                CourtTouchCount++;
                break;
        }
    }
}
