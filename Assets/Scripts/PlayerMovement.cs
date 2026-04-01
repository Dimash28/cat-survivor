using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);

        transform.position += moveDir * Time.deltaTime * speed; 
    }
}
