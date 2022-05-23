using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using participants = ProjectEnums.Enums.ParticipantsOfGame;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 4f;

    protected readonly float _sideforceMultiplier = 6f;
    protected readonly float _upperHitJoystickBound = 0.50f;
    protected readonly float _directHitForceMultiplier = 25f;
    protected readonly float _directHitUpMultiplier = 0.80f;

    protected float _sideMovementBound, _horizontalMovementBound, _verticalMovementBound;  

    [SerializeField] protected GameObject _ballPrefab;
    protected GameBehaviour _gameBehaviour;
    protected Animator _animator;
    protected HitJoystick _hitButton;
    protected Ball _ball;
    protected GameObject _ballClone;
    protected HitUpTouchZone _hitUpTouchZone;

    public bool HasHitAttempt { get; set; } = false;

    protected void InitializeBounds(float side, float horizontal,float vertical)
    {
        _sideMovementBound = side;
        _horizontalMovementBound = horizontal;
        _verticalMovementBound = vertical;
    }
    protected void InitializeReferences()
    {
        _gameBehaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _hitButton = GameObject.Find("Hit_Joystick").GetComponent<HitJoystick>();
        _animator = GetComponent<Animator>();
        _ball = _ballPrefab.GetComponent<Ball>();
    }
    protected void SpawnBall()
    {
       GameObject ball = Instantiate(_ballPrefab, transform.position + new Vector3(0, 2f, 0.2f), _ballPrefab.transform.rotation);
        _gameBehaviour.ballsInScene.Add(ball);
    }

    protected void StartHitAnimation()
    {
        _animator.SetTrigger("Hit_Trigger");
        HasHitAttempt = true;
        Invoke("FinishHitAttempt", 0.35f);
    }  
    protected void SubscribeToHitButtonEvents() => _hitButton.ButtonClicked += StartHitAnimation;
    protected void Move()
    {
        _ballClone = GameObject.Find("Ball(Clone)");
        Vector3 direction = _ballClone.transform.position - transform.position;

       if(GameBehaviour.LastTouched == participants.Enemy)
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
        float sideForce = _hitButton.HandleHorizontalPosition * _sideforceMultiplier;
        float upForce = (HitUpTouchZone.IsTouched == true) ? 8f : 4f;
        float forwardForce = (HitUpTouchZone.IsTouched == true) ? 8f :12f ;

         float[] forces = { sideForce, upForce, forwardForce };
        return forces;
    }
    protected void FinishHitAttempt() => HasHitAttempt = false;
}
