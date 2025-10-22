using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceDetector : MonoBehaviour
{
    public static bool IsUsingGamepad { get; private set; }
    
    void Update()
    {
        // ��������� ���� � �������� (������ Input System)
        if (Gamepad.current != null)
        {
            IsUsingGamepad = true;
        }

        // ��������� ���� � ����������
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            IsUsingGamepad = false;
        }
    }
}
