using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;

class EnemyBehaviour : Enemy
{
    private void Awake()
    {
        SetDifficulty();
        
    }
    private void Start()
    {
        InitializeBounds(7f, 9f,0.7f);
        InitializeReferences();
        SetSpeed();
        SetBallVisibility();

        if (GameBehaviour.NextRoundBeginner == participants.Enemy && TutorialBehaviour.isIncluded == false)
        {
            SpawnBall();
            Invoke("StartHitAnimation", 2f);
        }

       
    }

    private void Update()
    {
        if(_ballClone != null)
        {
            DistanceToBall = Vector3.Distance(transform.position, _ballClone.transform.position);
            Debug.Log(DistanceToBall);

            if (HasHitAttempt == false && DistanceToBall < 2f)
                StartHitAnimation();
        }
        
        switch (_gameBehaviour.RoundHasBegan == true)
        {
            case true:
                Move();
                RestrictMovement();
                break;


            case false:
                RestrictMovement();
                break;
        }
    }   
}
