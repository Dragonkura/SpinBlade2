using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private Rigidbody _rb;
    private BoxCollider _collider;
    private Coroutine _coroutine;
    public ItemType itemType;
    private bool ableToTrigger;
    public enum ItemType
    {
        ItemWeapon,
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagNameConst.COLLECTABLE_WEAPON) return;
        if(gameObject.activeSelf) _coroutine = StartCoroutine(ISetAbleToCollect());
    }
    IEnumerator ISetAbleToCollect()
    {
        var objectMaterial = gameObject.GetComponent<MeshRenderer>().material;
        // Set the material's transparency to 50%
        objectMaterial.color = new Color(objectMaterial.color.r, objectMaterial.color.g, objectMaterial.color.b, 25f/255);
        var delayTime = 3f;
        objectMaterial.color = new Color(objectMaterial.color.r, objectMaterial.color.g, objectMaterial.color.b, 1f);

        yield return new WaitForSeconds(delayTime);
        SetTrigger(true);
        _coroutine = null;
        yield break;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!ableToTrigger) return;
        switch (itemType)
        {
            case ItemType.ItemWeapon:
                var wp = other.GetComponent<WeaponElement>();
                if((wp != null))
                {
                    if (wp.controller.GetComponent<Player>() != null)
                    {
                        wp.controller.AddWeaponToOwner(1);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        
                    }
                }
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        SetTrigger(false);

    }

    private void SetTrigger(bool setment)
    {
        ableToTrigger = setment;
    }
}
