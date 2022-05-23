using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HitUpTouchZone : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static bool IsTouched { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData) => IsTouched = true;
    public void OnPointerUp(PointerEventData eventData) => IsTouched = false;   
}
