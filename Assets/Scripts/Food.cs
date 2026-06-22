using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private SoundSO foodPickupSoundSO;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            G.audio.Play(foodPickupSoundSO);
            G.player.Heal(healAmount);
            
            Destroy(gameObject);
        }
    }
}
