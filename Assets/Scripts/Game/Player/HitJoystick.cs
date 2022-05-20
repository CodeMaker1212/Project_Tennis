using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class HitJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public delegate void HitJoysticknEvent();
    public event HitJoysticknEvent ButtonClicked;
    public event HitJoysticknEvent ButtonClickStarted;

    private VariableJoystick _varJoystickScript;

    public float HandleHorizontalPosition { get; private set; }
    public float ClickTime { get; private set; }
    private bool _isClicked = false;
    public bool PreparingForHit { get; private set; } = false;

    private void Start()
    {
        _varJoystickScript = GetComponent<VariableJoystick>();
    }

    void Update() 
    {
        if(_isClicked == true) MarkClickTime();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PreparingForHit = true;

        ClickTime = 0;
        _isClicked = true;
        ButtonClickStarted();       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PreparingForHit=false;

        Invoke("ResetHandlePositionData", 0.2f);
        _isClicked = false;
        ButtonClicked();       
    }
    private void MarkClickTime() => ClickTime += Time.deltaTime;

    public void OnDrag(PointerEventData eventData) => HandleHorizontalPosition = _varJoystickScript.Horizontal;

    private void ResetHandlePositionData() => HandleHorizontalPosition = 0f;
    
}
