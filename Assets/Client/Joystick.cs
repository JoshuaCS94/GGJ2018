using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	private const float FADE_DURATION = .2f;

	public bool fixedPosition;

	public Vector2 Value { get; private set; }

	private RectTransform m_movArea;
	private float m_movAreaSize;

	private RectTransform m_deadArea;
	private float m_deadAreaSizeSqr;

	private RectTransform m_nipple;

	private Sequence m_animation;

	// Use this for initialization
	private void Start()
	{
		var canvasScale = GameObject.Find("Canvas").transform.localScale;

		m_movArea = transform.GetChild(0) as RectTransform;
		m_movAreaSize = m_movArea.rect.width * canvasScale.x / 2;

		m_deadArea = transform.GetChild(1) as RectTransform;
		var deadAreaSize = m_deadArea.rect.width * canvasScale.x / 2;
		m_deadAreaSizeSqr = deadAreaSize * deadAreaSize;

		m_nipple = transform.GetChild(2) as RectTransform;

		var movAreaImg = m_movArea.GetComponent<Image>();
		var deadAreaImg = m_deadArea.GetComponent<Image>();
		var nippleImg = m_nipple.GetComponent<Image>();

		m_animation = DOTween.Sequence()
			.Join(movAreaImg.DOFade(0, FADE_DURATION))
			.Join(deadAreaImg.DOFade(0, FADE_DURATION))
			.Join(nippleImg.DOFade(0, FADE_DURATION))
			.Pause()
			.SetAutoKill(false)
			.SetEase(Ease.Linear);

		m_animation.Complete();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (fixedPosition)
		{

		}
		else
		{
			m_movArea.position = m_deadArea.position = m_nipple.position = eventData.position;

			m_animation.PlayBackwards();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_nipple.position = m_movArea.position;

		Value = Vector2.zero;

		m_animation.PlayForward();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (fixedPosition)
		{}
		else
		{
			var movAreaPos = (Vector2) m_movArea.position;

			var delta = Vector2.ClampMagnitude(eventData.position - movAreaPos, m_movAreaSize);

			if (delta.sqrMagnitude < m_deadAreaSizeSqr)
			{
				m_nipple.position = movAreaPos;

				Value = Vector2.zero;
			}
			else
			{
				m_nipple.position = movAreaPos + delta;

				Value = delta / m_movAreaSize;
			}
		}
	}
}
