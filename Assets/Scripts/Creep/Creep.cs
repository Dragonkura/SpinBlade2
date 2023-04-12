using UnityEngine;
[System.Serializable]
public class Creep : WeaponOwner
{
    public int weaponOwn;
    public CreepType creepType;
    public bool isArgo = false;
    public Transform target;
    public Vector3 startPos;
    public override void Start()
    {
        switch (creepType)
        {
            case CreepType.Small:
                weaponOwn = 1;
                break;
            case CreepType.Normal:
                weaponOwn = 3;
                break;
            case CreepType.Big:
                weaponOwn = 5;
                break;
            default:
                break;
        }
        weaponSpawner.StartWeaponAmount = weaponOwn;
        base.Start();
    }
    public void MoveToTarget(Vector3 target)
    {
        if((target - transform.position).sqrMagnitude < 0.1f)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        var direction = (target - transform.position).normalized;
        transform.position += direction * Time.fixedDeltaTime * speed;
    }
    public void Agro(GameObject target)
    {

    }
    public override void AddWeaponToOwner(int amount)
    {

    }
    public override void Move()
    {
    }
    private void FixedUpdate()
    {
        //if (!isArgo) return;
        if (isArgo)
        {
            if(target != null) MoveToTarget(target.position);
        }
        else
        {
            MoveToTarget(startPos);
        }
    }
}
public enum CreepType
    {
        Small,
        Normal,
        Big,
    }
