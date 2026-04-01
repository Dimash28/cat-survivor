using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    private PlayerInputSystem playerInputSystem;

    private void Awake()
    {
        Instance = this;

        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputSystem.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
