using System.Collections;
using UnityEngine;
using Pixelplacement;
public class PlayerController : Singleton<PlayerController>
{
    public BaseMovement currentTarget;
    private void Update()
    {
        if(currentTarget != null)
            currentTarget.Direction = InputManager.Instance.directInput;
    }
}