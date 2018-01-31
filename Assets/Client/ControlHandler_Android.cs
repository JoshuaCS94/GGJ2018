using UnityEngine;

public class ControlHandler_Android : MonoBehaviour, IControlHandler
{
    public Vector2 Movement { get; set; }
    public KeyCode Burst { get; set; }

    private Joystick m_joystick;
    private ControlButton m_jumpBtn;
    private ControlButton m_burstBtn;

    private void Awake()
    {
        m_joystick = transform.GetChild(0).GetComponent<Joystick>();
        m_jumpBtn = transform.GetChild(1).GetComponent<ControlButton>();
        m_burstBtn = transform.GetChild(2).GetComponent<ControlButton>();
    }

    // Update is called once per frame
    void Update () {

    }
}
