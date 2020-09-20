﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform stick;
    [SerializeField] Image background;
    public float joystickLimit;
    Vector2 ConvertToLocalPos(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.enterEventCamera, out newPos);
        return newPos;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 stickPos = ConvertToLocalPos(eventData);    
        if(stickPos.magnitude > joystickLimit)
        {
            stickPos = stickPos.normalized * joystickLimit;
        }
        if(stick!=null)
            stick.anchoredPosition = stickPos;

        float x = stickPos.x / joystickLimit;
        float y = stickPos.y / joystickLimit;

        InputManager.Instance.SetAxis("Horizontal", x);
        InputManager.Instance.SetAxis("Vertical", y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(stick!=null && background != null)
        {
            background.color = Color.blue;
            stick.anchoredPosition = ConvertToLocalPos(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (stick != null && background != null)
        {
            background.color = Color.gray;
            stick.anchoredPosition = Vector2.zero;
        }
        InputManager.Instance.SetAxis("Horizontal", 0);
        InputManager.Instance.SetAxis("Vertical", 0);
    }

    void OnDisable()
    {
        InputManager.Instance.SetAxis("Horizontal", 0);
        InputManager.Instance.SetAxis("Vertical", 0);
    }
}
