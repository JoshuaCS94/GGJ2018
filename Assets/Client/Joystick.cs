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
	private RectTransform m_deadArea;
	private RectTransform m_nipple;

	private Sequence m_animation;

	// Use this for initialization
	private void Start()
	{
		m_movArea = transform.GetChild(0) as RectTransform;
		m_deadArea = transform.GetChild(1) as RectTransform;
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
		var movAreaSize = m_movArea.rect.width * m_movArea.transform.lossyScale.x / 2;
		var deadAreaSize = m_deadArea.rect.width * m_deadArea.transform.lossyScale.x / 2;
		var deadAreaSizeSqr = deadAreaSize * deadAreaSize;

		if (fixedPosition)
		{}
		else
		{
			var movAreaPos = (Vector2) m_movArea.position;

			var delta = Vector2.ClampMagnitude(eventData.position - movAreaPos, movAreaSize);

			Value = delta.sqrMagnitude < deadAreaSizeSqr ? Vector2.zero : delta / movAreaSize;

			m_nipple.position = movAreaPos + delta;
		}
	}
}
