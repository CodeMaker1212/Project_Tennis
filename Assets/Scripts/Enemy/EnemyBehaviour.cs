using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;

class EnemyBehaviour : Enemy
{
    private void Start()
    {
        InitializeBounds(7f, 9f,0.7f);
        InitializeReferences();

        if (GameBehaviour.NextRoundBeginner == participants.Enemy)
        {
            SpawnBall();
            Invoke("StartHitAnimation", 2f);
        }
           
    }

    private void Update()
    {
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
