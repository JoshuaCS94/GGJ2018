using UnityEngine;

public class ControlHandler_Standalone : MonoBehaviour, IControlHandler
{
    public int Movement { get; set; }
    public bool Jump { get; set; }
    public KeyCode Burst { get; set; }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleBurst();
    }

    private void HandleMovement()
    {
        var m = Input.GetAxisRaw("Horizontal");

        Movement = Mathf.Approximately(m, 0) ? 0 : System.Math.Sign(m);
    }

    private void HandleJump()
    {
        Jump = Input.GetKeyDown(KeyCode.W);
    }

    private void HandleBurst()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Burst = KeyCode.UpArrow;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Burst = KeyCode.RightArrow;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Burst = KeyCode.LeftArrow;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Burst = KeyCode.DownArrow;
        else
            Burst = KeyCode.None;
    }
}
