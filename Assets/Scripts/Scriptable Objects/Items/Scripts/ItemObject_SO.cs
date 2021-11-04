using UnityEngine;

public enum ItemType
{
    Weapons,
    Armor,
    Castles,
    Protection,
    Eqipment,
    Treasures,
    Default
}

[System.Serializable]
public abstract class ItemObject_SO : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(5, 10)]
    public string description;
}
