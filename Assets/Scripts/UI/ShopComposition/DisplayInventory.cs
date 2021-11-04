using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DisplayInventory : MonoBehaviour
{
    public InventoryObject_SO inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUM_OF_COLUMN;
    string makeap = "n0";
    int i;
    int interval = 9;
    Dictionary<InventorySlots, GameObject> itemDisplayed = new Dictionary<InventorySlots, GameObject>();

    void Start()
    {
        CreateDisplay();
        gameObject.SetActive(false);

    }
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            UpdateDisplay();
        }
    }
    public void ActivateShop() => gameObject.SetActive(true);

    public void UpdateDisplay()
    {

        for (i = 0; i < inventory.Container.Count; i++)
        {

            // Time.frameCount
            if (itemDisplayed.ContainsKey(inventory.Container[i]))
            {
                Debug.Log("TEST");
                itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString(makeap);
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString(makeap);
                itemDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {

            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString(makeap);
            itemDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        if (i == 2)
        {
            Y_SPACE_BETWEEN_ITEM += 45;
        }
        else if (i == 3)
        {
            Y_SPACE_BETWEEN_ITEM -= 12;
        }
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i / NUM_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i % NUM_OF_COLUMN)), 0f);
    }
}
