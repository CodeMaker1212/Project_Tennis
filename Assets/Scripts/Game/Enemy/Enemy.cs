using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using difficulty = ProjectEnums.Enums.DifficultyLevel;

public class Enemy : MonoBehaviour
{
    private const float _easySpeed = 3f;
    private const float _hardSpeed = 6f;

    private const float _easyBallVisibility = 0f;
    private const float _hardBallVisibility = -8f;

    protected const float _easyforceMultiplier = 2f;
    protected const float _hardforceMultiplier = 4f;

    protected float _moveSpeed;

   
    protected float _sideMovementBound, _horizontalMovementBound, _verticalMovementBound;
   
    [SerializeField] protected GameObject _ballPrefab;
    protected GameBehaviour _gameBehaviour;
    protected Animator _animator; 
    protected GameObject _ballClone;
    
    public difficulty Difficulty { get; private set; }
    public float BallVisibilityPoint { get; private set; }

    public bool HasHitAttempt { get; set; } = false;

    protected void SetDifficulty() => Difficulty = MainManager.Instance.chosenDifficulty;
    protected void SetSpeed()
    {
        switch (Difficulty)
        {
            case difficulty.EASY: _moveSpeed = _easySpeed;
                break;
            case difficulty.HARD: _moveSpeed = _hardSpeed;
                break;
        }
    }
    protected void SetBallVisibility()
    {
        switch (Difficulty)
        {
            case difficulty.EASY: BallVisibilityPoint = _easyBallVisibility;
                break;
            case difficulty.HARD: BallVisibilityPoint = _hardBallVisibility;
                break;
            default:
                break;
        }
    }
    protected void InitializeBounds(float side, float horizontal, float vertical)
    {
        _sideMovementBound = side;
        _horizontalMovementBound = horizontal;
        _verticalMovementBound = vertical;
    }
    protected void InitializeReferences()
    {
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _animator = GetComponent<Animator>();
       
    }
    protected void SpawnBall()
    {
      GameObject ball = Instantiate(_ballPrefab, transform.position, _ballPrefab.transform.rotation);
        _gameBehaviour.ballsInScene.Add(ball);

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

        if(_ballClone.transform.position.z > BallVisibilityPoint)
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
        if(Difficulty == difficulty.EASY)
        {
            float sideForce = Random.Range(-2, 3) *_easyforceMultiplier;
            float upForce = Random.Range(3, 5) * _easyforceMultiplier;
            float forwardForce = -4 * _easyforceMultiplier;

            float[] forces = { sideForce, upForce, forwardForce };
            return forces;
        }
        else  
        {
            float sideForce = (transform.position.x == _horizontalMovementBound || transform.position.x == -_horizontalMovementBound) ? 0 : Random.Range(-1, 2);
            float upForce = (transform.position.x > 2f || transform.position.x <-2f) ?  _hardforceMultiplier :Random.Range(2, 3) *_hardforceMultiplier;
            float forwardForce = (transform.position.x >2f || transform.position.x <-2f) ? -5 * _hardforceMultiplier:-3 * _hardforceMultiplier;

            float[] forces = { sideForce, upForce, forwardForce };
            return forces;
        }     
    }
    

    private void FinishHitAttempt()
    {
        HasHitAttempt = false;
    }


}
