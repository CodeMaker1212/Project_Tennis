using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviour :Player
{
    private void Start()
    {
        InitializeBounds(7f, -9f, 0.20f);
        InitializeReferences();
        SubscribeToHitButtonEvents();


        if (_gameBahaviour.PlayerHitsFirst == true)
        SpawnBall();

    }
   

    private void Update()
    {
        switch (_gameBahaviour.RoundHasBegan == true)
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

    }

   

}
