using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool m_recentDown;
	private bool m_clearRecentDown;
	private bool m_recentUp;
	private bool m_clearRecentUp;

	// Update is called once per frame
	private void Update()
	{
		if (m_recentDown)
			if (m_clearRecentDown)
				m_recentDown = false;
			else m_clearRecentDown = true;

		if (!m_recentUp) return;

		if (m_clearRecentUp)
			m_recentUp = false;
		else m_clearRecentUp = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		m_recentDown = true;
		m_clearRecentUp = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_recentUp = true;
		m_clearRecentDown = false;
	}

	public bool GetButton()
	{
		return m_recentDown || m_clearRecentDown;
	}

	public bool GetButtonDown()
	{
		return m_recentDown;
	}

	public bool GetButtonUp()
	{
		return m_recentUp;
	}
}
