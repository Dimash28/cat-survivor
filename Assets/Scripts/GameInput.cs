using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public event EventHandler OnEscapePerformed;

    private PlayerInputSystem playerInputSystem;

    private void Awake()
    {
        Instance = this;

        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Player.Enable();
        playerInputSystem.Player.Escape.performed += Escape_Performed;
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputSystem.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }

    public void Escape_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEscapePerformed?.Invoke(this, EventArgs.Empty);
    }
}
