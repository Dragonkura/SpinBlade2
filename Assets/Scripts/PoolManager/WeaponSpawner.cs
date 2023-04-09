using System;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class WeaponSpawner : MonoBehaviour
{
    public Action OnPooledObject;
    public Transform holder;
    [SerializeField] int startWeaponAmount = 10;
    public int StartWeaponAmount
    {
        get { return startWeaponAmount; }
        set { startWeaponAmount = value; }
    }

    [SerializeField] float minRadius = 2f;
    public WeaponOwner controller;

    private void Awake()
    {
        controller = transform.parent.gameObject.GetComponent<WeaponOwner>();
    }   
    public void Init()
    {
        for (int i = 0; i < startWeaponAmount; i++)
        {
            SpawnWeapon();
        }
    }
    public int GetOwnWeaponsAmount()
    {
        return activeWeaponAmount;
    }
    public void SpawnWeapon(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            var obj = PoolManager.GetPoolByName(PoolNameConst.ROTATING_WEAPON).GetPooledObject(true);
            obj.transform.SetParent(holder);
            obj.GetComponent<RotatingWeapon>().controller = controller;
            var wp = obj.GetComponent<RotatingWeapon>();
            wp.OnDeactive -= CaculateNewTranform;
            wp.OnDeactive += CaculateNewTranform;
        }
        
        CaculateNewTranform();
        OnPooledObject?.Invoke();
    }

    public int activeWeaponAmount = 0;
    private void CaculateNewTranform()
    {
        var pooledObjects = GetComponentsInChildren<RotatingWeapon>();

        List<RotatingWeapon> lstActiveObject = new List<RotatingWeapon>();
        for (int i = 0; i < pooledObjects.Length; i++)
        {
            if (pooledObjects[i].gameObject.activeSelf) lstActiveObject.Add(pooledObjects[i]);
        }
        activeWeaponAmount = lstActiveObject.Count;
        if (activeWeaponAmount == 0) return;
        var rotationBase = new Vector3(0, 1, 0) * (360f / activeWeaponAmount);
        float radiusMultiplier = 1.5f;
        var radius = radiusMultiplier * activeWeaponAmount / (2 * Mathf.PI * 2);//(radiusMultiplier * (1 + (0.5f * objectAmount / startWeaponAmount)));
        if (radius < minRadius) radius = minRadius;
        for (int i = 0; i < lstActiveObject.Count; i++)
        {
            var obj = lstActiveObject[i];
            obj.transform.SetRotation(rotationBase * (i + 1));
            obj.transform.localPosition = Vector3.zero;
            obj.Init();
            var rotatingWeapon = obj.weaponElement;
            rotatingWeapon.transform.SetLocalPosX(radius);
        }
    }
}
