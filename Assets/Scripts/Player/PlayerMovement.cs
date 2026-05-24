using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();

        transform.position += (Vector3)inputVector * Time.deltaTime * speed; 
    }
}
