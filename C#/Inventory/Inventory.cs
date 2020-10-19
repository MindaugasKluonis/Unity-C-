using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private IItem[] items;
    private int inventorySize = 100;
    private int currentSize;

    public Inventory()
    {
        currentSize = 0;
        items = new IItem[inventorySize];
    }

    public bool IsInventoryFull()
    {
        if(currentSize >= inventorySize)
        {
            return true;
        }

        return false;
    }

    public void AddItem(int index, IItem item)
    {
        items[index] = item;
        currentSize++;
    }

    public void RemoveItem(int index)
    {
        items[index] = null;
        currentSize--;
    }

    public IItem GetItem(int index)
    {
        return items[index];
    }

    public int Contains(IItem item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] != null)
            {
                if (items[i].ItemID == item.ItemID)
                {
                    return i;
                }

            }
        }

        return -1;
    }

    public int GetInventorySize()
    {
        return inventorySize;
    }
}
