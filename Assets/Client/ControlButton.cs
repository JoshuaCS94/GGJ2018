using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool m_down;
	private bool m_downNow;
	private bool m_upNow;

	private void LateUpdate()
	{
		m_downNow = false;
		m_upNow = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		m_down = m_downNow = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_down = false;
		m_upNow = true;
	}

	public bool GetButton()
	{
		return m_down;
	}

	public bool GetButtonDown()
	{
		return m_downNow;
	}

	public bool GetButtonUp()
	{
		return m_upNow;
	}
}
