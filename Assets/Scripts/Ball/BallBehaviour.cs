using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : Ball
{
    public event DoubleTouchEvent DoubleTouch;

    private void Start()
    {
        InitializeReferences();
    }

    private void Update()
    {
        if(_gameBehaviour.RoundHasBegan == false && _gameBehaviour.RoundIsOver == false)
        {
            if (_gameBehaviour.PlayerHitsFirst == true)
                FollowThePlayer();
            else FollowTheEnemy();
        }


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
                GameBehaviour.LastTouched = "Player";
                break;



            case "Enemy":
                ResetTouchCount();
                BounceOffEnemy();
                GameBehaviour.LastTouched = "Enemy";
                break;



            case "net":
                BounceOffNet();

                if (_gameBehaviour.RoundHasBegan == true)
                {

                   

                    if (GameBehaviour.LastTouched == "Player")
                    {
                        ScoreManager.DeleteScoreFrom("Player");
                        _gameBehaviour.ShowTouchingNetText("Player");
                    }
                    else if (GameBehaviour.LastTouched == "Enemy")
                    {
                        ScoreManager.DeleteScoreFrom("Enemy");
                        _gameBehaviour.ShowTouchingNetText("Enemy");
                    }
                  
                }
                break;





            case "Enemy_Border":
                ScoreManager.AddScoreTo("Player");
                _gameBehaviour.ShowWhoGetsPointText("Player");
                break;



            case "Player_Border":
                ScoreManager.AddScoreTo("Enemy");
                _gameBehaviour.ShowWhoGetsPointText("Enemy");
                break;



            case "Out_Borders":

                if(_gameBehaviour.RoundHasBegan == true)
                {

                    if (GameBehaviour.LastTouched == "Player")
                    {
                        ScoreManager.DeleteScoreFrom("Player");
                        _gameBehaviour.ShowOutText("Player");
                    }
                    else if (GameBehaviour.LastTouched == "Enemy")
                    {
                        ScoreManager.DeleteScoreFrom("Enemy");
                        _gameBehaviour.ShowOutText("Enemy");
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
