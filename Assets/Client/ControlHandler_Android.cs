using UnityEngine;

public class ControlHandler_Android : MonoBehaviour, IControlHandler
{
    public Vector2 Movement { get; private set; }

    public KeyCode? Burst { get; private set; }

    private void Update()
    {
        HandleMovement();
        HandleBurst();
    }

    private void HandleMovement()
    {
//            var mx = Input.GetAxisRaw("Horizontal");
//            var my = Input.GetAxisRaw("Vertical");
//
//            Movement = new Vector2(mx, my);
    }

    private void HandleBurst()
    {
//            if (Input.GetKeyDown(KeyCode.UpArrow))
//                Burst = KeyCode.UpArrow;
//            else if (Input.GetKeyDown(KeyCode.RightArrow))
//                Burst = KeyCode.RightArrow;
//            else if (Input.GetKeyDown(KeyCode.LeftArrow))
//                Burst = KeyCode.LeftArrow;
//            else if (Input.GetKeyDown(KeyCode.DownArrow))
//                Burst = KeyCode.DownArrow;
//            else
//                Burst = null;
    }
}
