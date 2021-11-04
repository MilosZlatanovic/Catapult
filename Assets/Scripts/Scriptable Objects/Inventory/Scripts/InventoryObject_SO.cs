using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject_SO : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject_SO database;
    public List<InventorySlots> Container = new List<InventorySlots>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject_SO)AssetDatabase.LoadAssetAtPath("Assets/Resources/Item Database.asset", typeof(ItemDatabaseObject_SO));
#else
        database = Resources.Load<ItemDatabaseObject_SO>("Assets/Resources/Item Database.asset");
#endif
    }

    public void AddItem(ItemObject_SO _item, int _amount)
    {

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlots(database.GetId[_item], _item, _amount));
    }
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];
    }

    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class InventorySlots
{
    public int ID;
    public ItemObject_SO item;
    public int amount;
    public InventorySlots(int _id, ItemObject_SO _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
