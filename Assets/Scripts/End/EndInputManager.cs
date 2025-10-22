using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EndInputManager : MonoBehaviour
{
    public static EndInputManager instance;
    public static PlayerInput PlayerInput { get; set; }

    public bool Confirm { get; set; }

    private InputAction _confirmAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerInput = GetComponent<PlayerInput>();
        _confirmAction = PlayerInput.actions["Confirm"];
    }
    private void Update()
    {
        Confirm = _confirmAction.WasPressedThisFrame();
    }
}
