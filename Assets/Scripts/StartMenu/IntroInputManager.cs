using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroInputManager : MonoBehaviour
{
    public static IntroInputManager instance;

    public Vector2 NavigationInput {  get;  set; }

    public bool CancelInput { get; set; }

    private InputAction _navigationAction;
    private InputAction _cancelAction;

    public static PlayerInput PlayerInput { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        PlayerInput = GetComponent<PlayerInput>();
        _navigationAction = PlayerInput.actions["Navigate"];
        _cancelAction = PlayerInput.actions["Cancel"];
    }
    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
        CancelInput = _cancelAction.WasPressedThisFrame();
    }
}
