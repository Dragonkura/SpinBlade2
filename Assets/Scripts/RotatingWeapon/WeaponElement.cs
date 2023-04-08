using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponElement : MonoBehaviour
{
    public Action onTriggerEnter;
    public Player controller;
    public int idGroup;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case TagNameConst.WEAPON:
                var wp = other.GetComponent<WeaponElement>();
                if (controller.GetInstanceID() != wp.controller.GetInstanceID())
                {
                    LauchWeapon();
                    onTriggerEnter?.Invoke();
                }
                break;
            default:
                break;
        }
        if (controller != null) controller.CheckPlayerState();
    }
    [SerializeField] float forceStrength = 0.01f;

    void LauchWeapon()
    {
        var obj = Watermelon.PoolManager.GetPoolByName(PoolNameConst.COLLECTABLE_WEAPON).GetPooledObject(true);
        var temp = transform.position;
        temp.y += 1;
        obj.transform.position = temp;

        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * forceStrength, ForceMode.Force);
    }
}
