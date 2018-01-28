using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ControlButton_Horizontal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float value;

    private IControlHandler m_controlHandler;

    private void Start()
    {
        m_controlHandler = GameObject.Find("Game Manager").GetComponent<IControlHandler>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_controlHandler.Movement = new Vector2(value, m_controlHandler.Movement.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_controlHandler.Movement = new Vector2(0, m_controlHandler.Movement.y);
    }
}
