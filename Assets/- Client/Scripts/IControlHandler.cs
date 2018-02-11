using UnityEngine;

public interface IControlHandler
{
    float Movement { get; }
    bool Jump { get; }
    KeyCode Burst { get; set; }
}
