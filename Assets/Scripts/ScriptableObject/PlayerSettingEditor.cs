using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSetting", menuName = "ScriptableObjects/PlayerSetting", order = 1)]
public class PlayerSettingEditor : ScriptableObject
{
    public float weaponRotatingSpeed;
    public float moveSpeed;
}
