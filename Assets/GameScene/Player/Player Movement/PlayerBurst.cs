using UnityEngine;

public class PlayerBurst : MonoBehaviour
{
	public PlayerAnimationManager animations;
	public PlayerMovement movement;
	public float force;
	internal bool lockedBurst = false;

	public void Burst(KeyCode direction)
	{
		if (lockedBurst) return;

		switch (direction)
		{
			case KeyCode.LeftArrow:
			{
				movement.rb.AddForce(movement.transform.right.normalized * -force);
				break;
			}

			case KeyCode.RightArrow:
			{
				movement.rb.AddForce(movement.transform.right.normalized * force);
				break;
			}

			case KeyCode.UpArrow:
			{
				movement.rb.AddForce(Vector2.up * force);
				movement.BlockJump();
				break;
			}
			case KeyCode.DownArrow:
			{
				movement.rb.AddForce(Vector2.down * force);
				break;
			}
		}

		animations.Burst(direction);
	}

	internal void BlockBurst()
	{
		lockedBurst = true;
		Invoke("UnblockBurst", movement.burstBlockTime);
	}

	internal void UnblockBurst()
	{
		lockedBurst = false;
	}
}
