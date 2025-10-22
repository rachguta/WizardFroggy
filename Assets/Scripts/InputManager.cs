using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public static PlayerInput PlayerInput;

    
    //Player
    //_____________________________________________//
    public Vector2 PlayerMovement { get; set; }


    public bool ShootInput { get; set; }
    public bool OpenPauseMenuInput { get; set; }
    public bool ParryInput { get; set; }

    public bool InteractionInput { get; set; }

    public bool InvincibilityInput { get; set; }

    public bool InvincibilityRelease {  get; set; }


    private InputAction _playerMovement;
    private InputAction _shootingAction;
    private InputAction _parryAction;
    private InputAction _openPauseMenu;
    private InputAction _interaction;
    private InputAction _invincibilityActivity;
    
    //______________________________________________//

    //UI
    //_____________________________________________//
    public Vector2 UINavigationInput {  get;  set; }
    public bool UIClosePauseMenuInput { get; set; }

    private InputAction _UInavigationAction;
    private InputAction _UIclosePauseMenu;
    //_____________________________________________//

    //Dialogue
    //_____________________________________________//
    public bool OpenPauseFromDialogue { get; set; }
    public bool SwitchDialogueLine { get; set; }

    private InputAction _dialogueOpenMenu;
    private InputAction _switchDialogueLinesAction;
    //_____________________________________________//

    //Reading
    //_____________________________________________//
    public bool OpenPauseFromReading { get; set; }
    public bool Confirm {  get; set; }

    private InputAction _readingOpenMenu;
    private InputAction _confirmAction;
    //_____________________________________________//

    //Cutsecene
    //_____________________________________________//
    public bool OpenPauseFromCutscene { get; set; }

    private InputAction _cutsceneOpenMenu;

    //_____________________________________________//




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerInput = GetComponent<PlayerInput>();
        //Player
        
        _playerMovement = PlayerInput.actions["Move"];
        _shootingAction = PlayerInput.actions["Shoot"];
        _parryAction = PlayerInput.actions["Parry"];
        _openPauseMenu = PlayerInput.actions["OpenMenu"];
        _interaction = PlayerInput.actions["Interaction"];
        _invincibilityActivity = PlayerInput.actions["Invincibility"];

        //UI
        _UIclosePauseMenu = PlayerInput.actions["Pause"];
        _UInavigationAction = PlayerInput.actions["Navigate"];

        //Dialogue
        _dialogueOpenMenu = PlayerInput.actions["OpenMenuFromDialogue"];
        _switchDialogueLinesAction = PlayerInput.actions["NextDialogueLine"];

        //Reading
        _readingOpenMenu = PlayerInput.actions["OpenMenuFromReading"];
        _confirmAction = PlayerInput.actions["Confirm"];

        //Cutscene
        _cutsceneOpenMenu = PlayerInput.actions["OpenMenuFromCutscene"];

       

    }
    private void Update()
    {
        // Player Map
        PlayerMovement = _playerMovement.ReadValue<Vector2>();
        
        ShootInput = _shootingAction.WasPressedThisFrame();
        ParryInput = _parryAction.WasPressedThisFrame();
        OpenPauseMenuInput = _openPauseMenu.WasPressedThisFrame();
        InteractionInput = _interaction.WasPressedThisFrame();
        InvincibilityInput = _invincibilityActivity.WasPressedThisFrame();
        InvincibilityRelease = _invincibilityActivity.WasReleasedThisFrame();
        // UI Map
        UINavigationInput = _UInavigationAction.ReadValue<Vector2>();
        UIClosePauseMenuInput = _UIclosePauseMenu.WasPressedThisFrame();
        //Dialogue Map
        OpenPauseFromDialogue = _dialogueOpenMenu.WasPressedThisFrame();
        SwitchDialogueLine = _switchDialogueLinesAction.WasPressedThisFrame();
        //Reading Map
        OpenPauseFromReading = _readingOpenMenu.WasPressedThisFrame();
        Confirm = _confirmAction.WasPressedThisFrame();
        //Cutscene
        OpenPauseFromCutscene = _cutsceneOpenMenu.WasPressedThisFrame();


    }
}
