using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class EnemyBehaviour : Enemy
{
    private void Start()
    {
        InitializeBounds(7f, 9f,0.7f);
        InitializeReferences();

        if (_gameBahaviour.PlayerHitsFirst == false)
        {
            SpawnBall();
            Invoke("StartHitAnimation", 2f);
        }
           
    }

    private void Update()
    {
        switch (_gameBahaviour.RoundHasBegan == true)
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
