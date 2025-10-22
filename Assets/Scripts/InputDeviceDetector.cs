using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceDetector : MonoBehaviour
{
    public static bool IsUsingGamepad { get; private set; }
    
    void Update()
    {
        // Проверяем ввод с геймпада (старый Input System)
        if (Gamepad.current != null)
        {
            IsUsingGamepad = true;
        }

        // Проверяем ввод с клавиатуры
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            IsUsingGamepad = false;
        }
    }
}
