using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponOwner
{
    public VariableJoystick variableJoystick;
    public Transform character;
    public Action<Transform> OnPlayerMovement;
  
    public override void Move()
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
    public virtual void FixedUpdate()
    {
        Move();
    }
    public override void Die()
    {
        GameManager.instance.EndGame();
        OnPlayerMovement = null;
        base.Die();
    }
}
