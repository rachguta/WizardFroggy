using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite gamepadIcon;
    [SerializeField] private Sprite keyboardIcon;
    
    private void Update()
    {
        if (InputDeviceDetector.IsUsingGamepad)
        {
            GetComponent<SpriteRenderer>().sprite = gamepadIcon;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = keyboardIcon;
        }

    }
}
