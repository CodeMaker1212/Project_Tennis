using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;



public class PlayerBehaviour :Player
{
    private void Start()
    {
        InitializeBounds(7f, -9f, 0.20f);
        InitializeReferences();
        SubscribeToHitButtonEvents();


        if (GameBehaviour.NextRoundBeginner == participants.Player)
        SpawnBall();

    }
   

    private void Update()
    {
        switch (_gameBehaviour.RoundHasBegan == true)
        {
            case true:
                Move();
                Turn();
                RestrictMovement();
                break;
            case false:
                
                RestrictMovement();
                break;
        }

        if (_hitButton.PreparingForHit == true)
            PrepareForHit();
       

    }

   

}
