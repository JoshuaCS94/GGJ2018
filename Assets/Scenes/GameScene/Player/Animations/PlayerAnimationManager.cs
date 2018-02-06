using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour 
{
	public delegate void AnimationMethod(KeyCode side);
	public event AnimationMethod OnBurst;

	public void Burst(KeyCode side)
	{
		if (OnBurst != null) OnBurst(side);
	}
}
