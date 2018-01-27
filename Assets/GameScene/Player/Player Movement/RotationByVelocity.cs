using DG.Tweening;
using UnityEngine;

public class RotationByVelocity : MonoBehaviour
{
	public Ease ease;
	public PlayerMovement movement;
	public float MaxRotation;
	public float TweeningTime;
	
	private Tweener rotationTweener;
	
	private void Start()
	{
		rotationTweener = transform.DORotate(new Vector3(0, 0, 10), 1);
		rotationTweener.SetEase(ease);
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, movement.transform.position, 2);

		if (movement.grounded)
		{
			rotationTweener = transform.DORotate(
				new Vector3(0, 0, -MaxRotation * movement.rb.velocity.x / movement.MaxSpeed),
				TweeningTime);
		}

		else
		{
			rotationTweener = transform.DORotate(
				Vector3.zero, 
				TweeningTime);
		}
	}
}
