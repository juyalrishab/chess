using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
     public static PlayerInput playerInput;
    private InputAction clickPos;
    private InputAction clickCheck;

    public static Vector2 ClickedPosistion;
    public static bool Clicked;

    void Awake()
    {
       playerInput = GetComponent<PlayerInput>();

        clickPos = playerInput.actions["ClickPos"];
        clickCheck = playerInput.actions["Click"];

    }

    // Update is called once per frame
    void Update()
    {
        ClickedPosistion = clickPos.ReadValue<Vector2>();
        Clicked = clickCheck.WasPressedThisFrame();
    }
}
