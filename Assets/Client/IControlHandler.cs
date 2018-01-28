using UnityEngine;

public interface IControlHandler
{
    Vector2 Movement { get; set; }
    KeyCode? Burst { get; set; }
}
