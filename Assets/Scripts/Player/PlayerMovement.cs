using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<SoundSO> walkSound;
    [SerializeField] private float walkSoundCooldown = 0.5f;
    private float lastWalkSoundTime;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = G.input.GetInputVectorNormalized();
        float speed = G.stats.MoveSpeed;

        transform.position += (Vector3)inputVector * Time.deltaTime * speed; 

        if (inputVector != Vector2.zero && Time.time - lastWalkSoundTime > walkSoundCooldown)
        {
            G.audio.Play(walkSound[Random.Range(0, walkSound.Count)]);
            lastWalkSoundTime = Time.time;
        }
    }
}
