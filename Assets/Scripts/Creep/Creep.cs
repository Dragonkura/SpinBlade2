using UnityEngine;
[System.Serializable]
public class Creep : WeaponOwner
{
    public int weaponOwn;
    public CreepType creepType;
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
    public void MoveToTarget(Transform target)
    {
        var direction = (target.position - transform.position).normalized;
        rb.velocity += direction * Time.fixedDeltaTime * speed;
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
}
public enum CreepType
    {
        Small,
        Normal,
        Big,
    }
