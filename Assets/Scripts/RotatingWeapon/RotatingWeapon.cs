using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingWeapon : MonoBehaviour
{
    public Action OnDeactive;
    public Player controller;
    public WeaponElement weaponElement;
    public void Init()
    {
        if(weaponElement != null)
        {
            weaponElement.controller = controller;
            weaponElement.onTriggerEnter -= OnTrigger;
            weaponElement.onTriggerEnter += OnTrigger;
        }

    }
    public void OnTrigger()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        OnDeactive?.Invoke();
        OnDeactive = null;
        if(weaponElement != null) weaponElement.onTriggerEnter -= OnTrigger;
    }

}
