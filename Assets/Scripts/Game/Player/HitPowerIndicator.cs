using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPowerIndicator : MonoBehaviour
{
   
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private float xQuaternion = 1.68f;
    [SerializeField] private float wQuaternion = 2f;

    private GameObject _player;
    private HitJoystick _hitButton;
   
    private RawImage _backgroundIndicator;
    private Image _foregroundIndicator;

    
    private void Start()
    {
        _player = GameObject.Find("Player");
        _hitButton = GameObject.Find("Hit_Joystick").GetComponent<HitJoystick>();
        
        _backgroundIndicator = GetComponent<RawImage>();
        _foregroundIndicator= GameObject.Find("Hit_Power_Indicator_Foreground").GetComponent<Image>();

        _hitButton.ButtonClickStarted +=EnableOrDisableImage;
        _hitButton.ButtonClicked +=EnableOrDisableImage;

        EnableOrDisableImage();       
    }
    private void Update()
    {
        FollowPlayer();
        ChangeColor();
        Rotate();
        
    }

    private void FollowPlayer() => transform.position = _player.transform.position + _playerOffset;
    private void EnableOrDisableImage()
    {
        if(_backgroundIndicator.enabled == false && _foregroundIndicator.enabled == false)
        {
            _backgroundIndicator.enabled = true;
            _foregroundIndicator.enabled = true;
        }
        else
        {
            _backgroundIndicator.enabled = false;
            _foregroundIndicator.enabled = false;
        }         
    } 
   
    private void ChangeColor()
    {
        _foregroundIndicator.fillAmount = _hitButton.ClickTime;
        _foregroundIndicator.color = new Color(0.5f, 1f - _foregroundIndicator.fillAmount, 0f, 1f);

    }
    private void Rotate()
    {
        if(_hitButton.HandleHorizontalPosition != 0) transform.rotation = new Quaternion(xQuaternion, 0, -_hitButton.HandleHorizontalPosition, wQuaternion);
        else transform.rotation = new Quaternion(xQuaternion,0,0,wQuaternion);
        
    }
   
}
