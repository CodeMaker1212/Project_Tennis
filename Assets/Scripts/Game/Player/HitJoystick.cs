using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class HitJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    public delegate void HitJoysticknEvent();
    public event HitJoysticknEvent ButtonClicked;
    public event HitJoysticknEvent ButtonClickStarted;
    public event HitJoysticknEvent ButtonDrag;

    private VariableJoystick _varJoystickScript;

    public float HandleHorizontalPosition { get; private set; }
    public float HandleVerticalPosition { get; private set; }
    public bool PreparingForHit { get; private set; } = false;

    public void OnBeginDrag(PointerEventData eventData) => ButtonDrag();
    public void OnPointerDown(PointerEventData eventData) => ButtonClickStarted();
    public void OnPointerUp(PointerEventData eventData)
    {
        if (TutorialBehaviour.isIncluded == true && TutorialBehaviour.isCompleted == false)
            return;
        else
        ButtonClicked();
    }
    public void OnDrag(PointerEventData eventData)
    {
        HandleHorizontalPosition = _varJoystickScript.Horizontal;
        HandleVerticalPosition = _varJoystickScript.Vertical;
    }


    private void Start()
    {
        _varJoystickScript = GetComponent<VariableJoystick>();
    }
   
}
