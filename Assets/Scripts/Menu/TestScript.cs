using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Button button;
    private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    //void Start()
    //{
    //    //playerInputActions = new PlayerInputActions();
    //    //playerInputActions.GameManagment.Enable();
    //    //playerInputActions.GameManagment.SelectionButton.performed += Test;
    //}

      
      
    public void Test()
    {
        Debug.Log("Boom");
    }
}
