using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protection Object", menuName = "Shop System/Items/Protection")]
public class ProtectionObject_SO : ItemObject_SO
{
    public int attackBonus;
    public int defenceBonus;
    public void Awake()
    {
        type = ItemType.Protection;
    }
}
