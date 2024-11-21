using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script for button presses
public class InputController : MonoBehaviour
{
    public bool IsLaunching { get {return _isLaunching;} set {_isLaunching = value;} }
    public bool IsResetting { get {return _isResetting;} set {_isResetting = value;} }
    
    private InputMaster _controls;
    private InputAction _launch;
    private InputAction _reset;
    
    //States
    private bool _isLaunching;
    private bool _isResetting;

    private void Awake()
    {
        _controls = new InputMaster(); 
    }

    private void OnEnable()
    {
        //Initialise controls and assign input actions to their functions
        _controls.Enable();
        
        _launch = _controls.Player.Launch;
        _launch.Enable();
        _reset = _controls.Player.Reset;
        _reset.Enable();
        
        _launch.started += _ => LaunchInput(_); //Pass current context of action to function
        _launch.canceled += _ => LaunchInput(_);
        _reset.started += _ => ResetInput(_);
        _reset.canceled += _ => ResetInput(_);
    }

    private void OnDisable()
    {
        //Disable when no longer needed
        _controls.Disable();
        _launch.Disable();
        _reset.Disable();
    }
    
    //Functions for setting control states
    private void LaunchInput(InputAction.CallbackContext context)
    {
        //Read current state
        _isLaunching = context.ReadValueAsButton();
    }

    private void ResetInput(InputAction.CallbackContext context)
    {
        _isResetting = context.ReadValueAsButton();
    }
}
