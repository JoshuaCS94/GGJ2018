using UnityEngine;

public interface IControlHandler
{
    int Movement { get; }
    bool Jump { get; }
    KeyCode Burst { get; set; }
}
