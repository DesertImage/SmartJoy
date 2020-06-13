using System;
using UnityEngine;

namespace SmartJoy
{
    public interface IJoystick
    {
        event Action<Vector2> OnStartDrag;
        event Action<Vector2> OnEndDrag;
        event Action<Vector2> OnInputChanged;

        Vector2 Input { get; }
    }
}