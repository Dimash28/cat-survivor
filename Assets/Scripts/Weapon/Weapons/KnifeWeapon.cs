using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeWeapon : Weapon
{
    [SerializeField] private List<SoundSO> knifeSoundList;
    private Vector2 latestInputVector;

    private void Start()
    {
        latestInputVector = Vector2.right;
    }

    protected override void Update()
    {
        base.Update();

        Vector2 currentInput = G.input.GetInputVectorNormalized();
        
        if(currentInput != Vector2.zero)
        {
            latestInputVector = currentInput;
        }
    }

    protected override void Attack()
    {
        if(runtimeDataSO == null) return;

        for (int i = 0; i < runtimeDataSO.projectileCount; i++)
        {
            float angleOffset = (i - (runtimeDataSO.projectileCount - 1) / 2f) * 15f;

            GameObject knife = Instantiate(
                runtimeDataSO.prefab, 
                transform.position, 
                Quaternion.identity
            );

            Vector2 direction;

            direction = Quaternion.Euler(0, 0, angleOffset) * latestInputVector;

            Projectile proj = knife.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Setup(direction, runtimeDataSO);
            }
            else
            {
                Debug.LogError("Projectile component not found on prefab!");
            }
        }

        G.audio.Play(knifeSoundList[Random.Range(0, knifeSoundList.Count)]);
    }
}
