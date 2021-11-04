using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Shop System/Items/Database")]
public class ItemDatabaseObject_SO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject_SO[] Items;
    public Dictionary<ItemObject_SO, int> GetId = new Dictionary<ItemObject_SO, int>();
    public Dictionary<int, ItemObject_SO> GetItem = new Dictionary<int, ItemObject_SO>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemObject_SO, int>();
        GetItem = new Dictionary<int, ItemObject_SO>();

        for (int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
