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
				movement.blockedJump = true;
				Invoke("UnblockJump", movement.burstBlockTime);
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

	void UnblockJump()
	{
		movement.blockedJump = false;
	}
}
