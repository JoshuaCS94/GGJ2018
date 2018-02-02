using UnityEngine;

public class ControlHandler_Android : MonoBehaviour, IControlHandler
{
    public int Movement { get; set; }
    public bool Jump { get; set; }
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
    private void Update ()
    {
        Jump = m_jumpBtn.GetButtonDown();

        var flag1 = m_joystick.Value.x >  m_joystick.Value.y;
        var flag2 = m_joystick.Value.x > -m_joystick.Value.y;

        if (!(flag1 ^ flag2))
            Movement = Mathf.Approximately(m_joystick.Value.x, 0) ? 0 : System.Math.Sign(m_joystick.Value.x);

        if (m_burstBtn.GetButtonDown())
            if (flag1)
                Burst = flag2 ? KeyCode.LeftArrow : KeyCode.DownArrow;
            else
                Burst = flag2 ? KeyCode.UpArrow : KeyCode.RightArrow;
        else
            Burst = KeyCode.None;
    }
}
