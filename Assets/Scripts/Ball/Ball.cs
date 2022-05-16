using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball: MonoBehaviour
{
    public delegate void DoubleTouchEvent();

    [SerializeField] protected Vector3 _playerOffset = new Vector3(0,2.5f,0.1f);
    [SerializeField] protected Vector3 _enemyOffset = new Vector3(0, 2.5f, 0.1f);

    protected PlayerBehaviour _player;
    protected Enemy _enemy;
    protected GameBehaviour _gameBehaviour;
    protected Rigidbody _rb;

    public bool IsCollided { get; protected set; }
    public int CourtTouchCount { get; protected set; } = 0;

    protected void InitializeReferences()
    {   
        _player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        _enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _rb = GetComponent<Rigidbody>();
    }
   
    
    protected void FollowThePlayer() => transform.position = _player.transform.position + _playerOffset;
    protected void FollowTheEnemy() => transform.position = _enemy.transform.position + _enemyOffset;
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

      //  GameBehaviour.LastHit = "Player";
    }
    protected void BounceOffEnemy()
    {
        if (_gameBehaviour.RoundHasBegan == true || _gameBehaviour.PlayerHitsFirst == false)
        {
            _rb.velocity = Vector3.zero;

            if (_enemy.HasHitAttempt == false)
                _enemy.StartHitAnimation();

            TakeHit(_enemy.CalculateHitForces());

          //  GameBehaviour.LastHit = "Enemy";
        }
    }
    protected void BounceOffNet() => _rb.velocity = -_rb.velocity / 3f;
    protected void ResetTouchCount() => CourtTouchCount = 0;
}
