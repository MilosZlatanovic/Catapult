using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Shop System/Items/Weapons")]
public class WeaponsObject_SO : ItemObject_SO
{
    public int attackBonus;
    public int defenceBonus;
    public void Awake()
    {
        type = ItemType.Weapons;
    }
}
