using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class BurstPlayerAnimation : PlayerAnimation
{
	public GameObject BurstAnimationObject;
	public PlayerAnimationManager animations;
	public string StartTriggerStr;

	[Header("Offsets")] 
	public Vector3 leftOffset;
	public Vector3 rightOffset;
	public Vector3 upOffset;
	public Vector3 downOffset;
	
	private Animator burstAnimationAnimator;
	private SpriteRenderer burstSpriteRenderer;

	private void Awake()
	{
		burstAnimationAnimator = BurstAnimationObject.GetComponent<Animator>();
		burstSpriteRenderer = BurstAnimationObject.GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		animations.OnBurst += action;
	}

	private void OnDestroy()
	{
		animations.OnBurst -= action;
	}

	void action(KeyCode side)
	{
		burstAnimationAnimator.SetTrigger(StartTriggerStr);

		switch (side)
		{
			case KeyCode.LeftArrow:
				BurstAnimationObject.transform.localPosition = leftOffset;
				BurstAnimationObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
				burstSpriteRenderer.flipX = false;
				burstSpriteRenderer.flipY = false;
				break;
			case KeyCode.RightArrow:
				BurstAnimationObject.transform.localPosition = rightOffset;
				BurstAnimationObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
				burstSpriteRenderer.flipX = true;
				burstSpriteRenderer.flipY = false;
				break;
			case KeyCode.UpArrow:
				BurstAnimationObject.transform.localPosition = upOffset;
				BurstAnimationObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
				burstSpriteRenderer.flipX = false;
				burstSpriteRenderer.flipY = true;
				break;
			case KeyCode.DownArrow:
				BurstAnimationObject.transform.localPosition = downOffset;
				BurstAnimationObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
				burstSpriteRenderer.flipX = false;
				burstSpriteRenderer.flipY = false;
				break;
		}
	}
}
