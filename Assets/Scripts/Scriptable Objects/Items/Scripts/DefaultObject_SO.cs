using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Shop System/Items/Default")]
public class DefaultObject_SO : ItemObject_SO
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}
