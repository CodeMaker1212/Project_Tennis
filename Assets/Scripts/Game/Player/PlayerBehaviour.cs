using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using participants = ProjectEnums.Enums.ParticipantsOfGame;



public class PlayerBehaviour :Player
{
    private void Start()
    {
        InitializeBounds(7f, -9f,0.7f);
        InitializeReferences();
        SubscribeToHitButtonEvents();

        if (GameBehaviour.NextRoundBeginner == participants.Player || TutorialBehaviour.isIncluded == true)
        SpawnBall();
    }
    private void Update()
    {      
        Move();
        RestrictMovement();
    }
}
