using UnityEngine;
using UnityEngine.EventSystems;


public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public bool fixedPosition;

	private RectTransform m_movArea;
	private RectTransform m_nipple;
	private RectTransform m_deadArea;

	private float m_movAreaSize;
	private float m_deadAreaSize;

	// Use this for initialization
	private void Start()
	{
		m_movArea = transform.GetChild(0) as RectTransform;
		m_deadArea = transform.GetChild(1) as RectTransform;
		m_nipple = transform.GetChild(2) as RectTransform;

		m_movAreaSize = m_movArea.sizeDelta.x / 2;
		m_deadAreaSize = m_deadArea.sizeDelta.x / 2;

		print(m_movAreaSize);

		if (fixedPosition)
		{

		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (fixedPosition)
		{

		}
		else
		{
			m_movArea.position = m_deadArea.position = m_nipple.position = eventData.position;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (fixedPosition)
		{}
		else
		{
			var movAreaPos = (Vector2) m_movArea.position;
			var delta = eventData.position - movAreaPos;

			m_nipple.position = movAreaPos + Vector2.ClampMagnitude(delta, m_movAreaSize);
		}
	}
}
