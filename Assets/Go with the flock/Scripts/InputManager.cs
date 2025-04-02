using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pixelplacement;

public class InputManager : Singleton<InputManager>
{
    public Vector2 directInput;

    public void OnMove(InputValue inputValue)
    {
        directInput = inputValue.Get<Vector2>();
    }

    
}
