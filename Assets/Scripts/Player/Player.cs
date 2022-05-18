using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 10f;
    [SerializeField] protected float _turnSpeed = 200f;

    protected readonly float _forceMultiplier = 8f;
    protected readonly float _sideforceMultiplier = 6f;
    protected readonly float _directHitJoystickBound = 0.90f;
    protected float _directHitForceMultiplier = 4f;
    protected float _directHitUpMultiplier = 0.3f;
    protected float _sideMovementBound, _horizontalMovementBound, _turnBound;   

    [SerializeField] protected GameObject _ballPrefab;
    protected Rigidbody _rb;
    protected GameBehaviour _gameBahaviour;
    protected FixedJoystick _movementJoystick;
    protected Animator _animator;
    protected HitJoystick _hitButton;
    protected VariableJoystick _hitJoystick;
    protected Ball _ball;
    protected Image _forceIndicator;

    public bool HasHitAttempt { get; set; } = false;

    protected void InitializeBounds(float side, float horizontal, float turn)
    {
        _sideMovementBound = side;
        _horizontalMovementBound = horizontal;
        _turnBound = turn;
    }
    protected void InitializeReferences()
    {
        _gameBahaviour = GameObject.Find("Game Behaviour").GetComponent<GameBehaviour>();
        _movementJoystick = GameObject.Find("Movement_Joystick").GetComponent<FixedJoystick>();
        _hitButton = GameObject.Find("Hit_Joystick").GetComponent<HitJoystick>();
        _hitJoystick = GameObject.Find("Hit_Joystick").GetComponent<VariableJoystick>();
        _forceIndicator = GameObject.Find("Hit_Power_Indicator_Foreground").GetComponent<Image>();

        _animator = GetComponent<Animator>();
        _ball = _ballPrefab.GetComponent<Ball>();

    }
    protected void SpawnBall()
    {
       GameObject ball = Instantiate(_ballPrefab, transform.position, _ballPrefab.transform.rotation);
        _gameBahaviour.ballsInScene.Add(ball);
    }
    protected void StartHitAnimation()
    {
        _animator.SetTrigger("Hit_Trigger");
        HasHitAttempt = true;
        Invoke("FinishHitAttempt", 0.2f);
    }  
    protected void SubscribeToHitButtonEvents() => _hitButton.ButtonClicked += StartHitAnimation;
    protected void Move() => transform.Translate(new Vector3(_movementJoystick.Horizontal * _moveSpeed * Time.fixedDeltaTime, 0,0), Space.World);
    protected void RestrictMovement()
    {
        if (transform.position.x > _sideMovementBound) transform.position = new Vector3(_sideMovementBound, transform.position.y, transform.position.z);
        if (transform.position.x < -_sideMovementBound) transform.position = new Vector3(-_sideMovementBound, transform.position.y, transform.position.z);
        if (transform.position.z > _horizontalMovementBound) transform.position = new Vector3(transform.position.x, transform.position.y, _horizontalMovementBound);
        if (transform.position.z < _horizontalMovementBound) transform.position = new Vector3(transform.position.x, transform.position.y, _horizontalMovementBound);
    }
    protected void Turn()
    {
        transform.Rotate(new Vector3(0, 0, -1), _movementJoystick.Horizontal * _turnSpeed * Time.deltaTime, Space.World);

        if (transform.rotation.z > _turnBound)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, _turnBound, 1);
        else if (transform.rotation.z < -_turnBound)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -_turnBound, 1);
    }
    public float[] CalculateHitForces()
    {
        float sideForce = _hitButton.HandleHorizontalPosition * _forceIndicator.fillAmount * _sideforceMultiplier;

        float upForce = (_movementJoystick.Vertical > _directHitJoystickBound)
            ? _directHitUpMultiplier * _forceIndicator.fillAmount * _movementJoystick.Vertical * _forceMultiplier
            : _forceIndicator.fillAmount * _forceMultiplier;

        float forwardForce = (_movementJoystick.Vertical > _directHitJoystickBound)
            ? _forceIndicator.fillAmount * _forceMultiplier * _directHitForceMultiplier
            : _forceIndicator.fillAmount * _forceMultiplier;

        float[] forces = { sideForce, upForce, forwardForce };
        return forces;
    }
    protected void FinishHitAttempt()
    {
        HasHitAttempt = false;
    }
}
