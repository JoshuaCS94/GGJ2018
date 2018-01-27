using UnityEngine;

public interface IControlHandler
{
    Vector2 Movement { get; }
    KeyCode? Burst { get; }
}
