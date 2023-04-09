using System.Collections.Generic;
using UnityEngine;

public class WeaponOwner: MonoBehaviour
{
    public float speed;
    public WeaponSpawner weaponSpawner;
    public Rigidbody rb;
    public List<RotatingWeapon> ownWeapons;
    public virtual void Start()
    {
        weaponSpawner.Init();
    }
    public virtual void Move()
    {
    }
    public virtual void CheckState()
    {
        if (weaponSpawner.GetOwnWeaponsAmount() == 0) Die();
    }
    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
    public virtual void AddWeaponToOwner(int amount)
    {
        if (weaponSpawner != null) weaponSpawner.SpawnWeapon(amount);
    }
}