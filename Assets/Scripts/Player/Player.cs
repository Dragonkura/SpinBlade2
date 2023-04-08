using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<RotatingWeapon> ownWeapons;
    public float speed;
    public VariableJoystick variableJoystick;
    public Transform character;
    public Rigidbody rb;
    public WeaponSpawner weaponSpawner;
    public Action<Transform> OnPlayerMovement;

    public virtual void Move()
    {
        Vector3 direction = (Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal).normalized;
        rb.velocity = direction * speed * Time.fixedDeltaTime;

        var charRotation = Mathf.Atan2(direction.x, direction.z) * 180 / Mathf.PI + 180;
        if(character != null)
        {
            if(variableJoystick.isOnHandle)
            character.SetRotation(new Vector3(0, charRotation, 0));
        }
        OnPlayerMovement?.Invoke(transform);
    }

    public void CheckPlayerState()
    {
        if (weaponSpawner.GetOwnWeaponsAmount() == 0) Die();
    }
    public virtual void AddWeaponToOwner(int amount)
    {
        if (weaponSpawner != null) weaponSpawner.SpawnWeapon(amount);
    }
    public virtual void FixedUpdate()
    {
        Move();
    }
    public virtual void Die()
    {
        GameManager.instance.EndGame();
        gameObject.SetActive(false);
        OnPlayerMovement = null;
    }
}
