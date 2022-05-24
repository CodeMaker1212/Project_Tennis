using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour
{
    public delegate void DoubleTouchEvent();

    [SerializeField] protected Vector3 _playerOffset;
    [SerializeField] protected Vector3 _enemyOffset;

    protected PlayerBehaviour _player;
    protected Enemy _enemy;
    protected GameBehaviour _gameBehaviour;
    protected Rigidbody _rb;

    public int CourtTouchCount { get; protected set; } = 0;

    protected void InitializeReferences()
    {   
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _rb = GetComponent<Rigidbody>();
    }
   
 
    protected void TakeHit(float[] axisForces)
    {
        _rb.useGravity = true;

        if (_gameBehaviour.RoundHasBegan == false)
            _gameBehaviour.RoundHasBegan = true;

        _rb.AddForce(axisForces[0],axisForces[1], axisForces[2], ForceMode.Impulse);           
    }
   protected void BounceOffPlayer()
    {
        if (_player.HasHitAttempt == true)
        {
            _rb.velocity = Vector3.zero;
            TakeHit(_player.CalculateHitForces());
        }
        else _rb.velocity = -_rb.velocity / 2f;
    }
    protected void BounceOffEnemy()
    {
        _rb.velocity = Vector3.zero;
        TakeHit(_enemy.CalculateHitForces());
    }
    protected void BounceOffNet() => _rb.velocity = -_rb.velocity / 3f;
    protected void ResetTouchCount() => CourtTouchCount = 0;
}
