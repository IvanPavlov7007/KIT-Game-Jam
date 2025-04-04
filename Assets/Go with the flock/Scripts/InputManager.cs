using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pixelplacement;

public class InputManager : Singleton<InputManager>
{
    public Vector2 directInput;
    public bool mousePressed;
    public bool AcceptPressed;
    public bool DeclinePressed;

    public event System.Action onAccept;
    public event System.Action onDecline;
    public event System.Action onFullScreen;
    public event System.Action onMousePressed;

    public void OnMove(InputValue inputValue)
    {
        directInput = inputValue.Get<Vector2>();
    }

    public void OnClick(InputValue inputValue)
    {
        mousePressed = inputValue.isPressed;
        if (mousePressed)
            onMousePressed?.Invoke();
    }
    
    public void OnAccept(InputValue inputValue)
    {
        AcceptPressed = inputValue.isPressed;
        if (AcceptPressed)
            onAccept?.Invoke();
    }

    public void OnFullScreen(InputValue inputValue)
    {
        if (inputValue.isPressed)
            onFullScreen?.Invoke();
    }

    public void OnDecline(InputValue inputValue)
    {
        DeclinePressed = inputValue.isPressed;
        if (DeclinePressed)
        {
            onDecline?.Invoke();
        }
    }
}
