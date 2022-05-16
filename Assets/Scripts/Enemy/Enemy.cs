using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected readonly float _forceMultiplier = 2f;
    protected float _sideMovementBound, _horizontalMovementBound, _verticalMovementBound;
    [SerializeField] protected float _moveSpeed;

    [SerializeField] protected GameObject _ballPrefab;
    protected GameBehaviour _gameBahaviour;
    protected Animator _animator; 
    protected GameObject _ballClone;
    
    

    public bool HasHitAttempt { get; set; } = false;

    protected void InitializeBounds(float side, float horizontal, float vertical)
    {
        _sideMovementBound = side;
        _horizontalMovementBound = horizontal;
        _verticalMovementBound = vertical;
    }
    protected void InitializeReferences()
    {
        _gameBahaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _animator = GetComponent<Animator>();
       
    }
    protected void SpawnBall()
    {
      GameObject ball = Instantiate(_ballPrefab, transform.position, _ballPrefab.transform.rotation);
        _gameBahaviour.ballsInScene.Add(ball);

    }
    public void StartHitAnimation()
    {
        _animator.SetTrigger("Enemy_Hit_Trigger");
        HasHitAttempt = true;
        Invoke("FinishHitAttempt", 0.2f);
    }
    protected void Move()
    {
        _ballClone = GameObject.Find("Ball(Clone)");
        Vector3 direction = _ballClone.transform.position - transform.position;

        if(_ballClone.transform.position.z > -2)
        transform.Translate(direction * _moveSpeed * Time.fixedDeltaTime, Space.World);
    }
    protected void RestrictMovement()
    {
        if (transform.position.x > _sideMovementBound) transform.position = new Vector3(_sideMovementBound, transform.position.y, transform.position.z);
        if (transform.position.x < -_sideMovementBound) transform.position = new Vector3(-_sideMovementBound, transform.position.y, transform.position.z);
        if (transform.position.y > _verticalMovementBound || transform.position.y < _verticalMovementBound) transform.position = new Vector3(transform.position.x, _verticalMovementBound, transform.position.z);
        if (transform.position.z > _horizontalMovementBound) transform.position = new Vector3(transform.position.x, transform.position.y, _horizontalMovementBound);
        if (transform.position.z < _horizontalMovementBound) transform.position = new Vector3(transform.position.x, transform.position.y, _horizontalMovementBound);
    }
   
     public float[] CalculateHitForces()
    {
        float sideForce = Random.Range(-2,3) * _forceMultiplier;

        float upForce = Random.Range(3, 5) * _forceMultiplier;

        float forwardForce = 4 * - _forceMultiplier;

        float[] forces = { sideForce, upForce, forwardForce };
        return forces;
    }
    private void FinishHitAttempt()
    {
        HasHitAttempt = false;
    }


}
