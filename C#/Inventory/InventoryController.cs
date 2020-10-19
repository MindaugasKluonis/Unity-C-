using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private Inventory inventory;

    public delegate void OnNewItemAddedDelegate(IItem item, int index);
    public event OnNewItemAddedDelegate OnNewItemAddedDelegateEvent = delegate { };

    public delegate void OnItemRemovedDelegate(IItem item, int index);
    public event OnItemRemovedDelegate OnItemRemovedDelegateEvent = delegate { };

    public delegate void OnFailedDelegate(InventoryResponeType args);
    public event OnFailedDelegate OnFailedDelegateEvent = delegate { };

    //OnItemUpdatedDelegateEvent.Invoke(item, index, stack);
    public InventoryResponeType AddItem(IItem item)
    {
        if (inventory.IsInventoryFull()) return InventoryResponeType.InventoryIsFull;

        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            if(inventory.GetItem(i) == null)
            {
                inventory.AddItem(i, item);
                OnNewItemAddedDelegateEvent.Invoke(item, i);
                return InventoryResponeType.ItemAdded;
            }
        }

        return InventoryResponeType.InventoryIsFull;
    }

    public InventoryResponeType RemoveItem(int index)
    {
        IItem item = inventory.GetItem(index);

        if (item != null)
        {
            inventory.RemoveItem(index);
            OnItemRemovedDelegateEvent.Invoke(item, index);
            return InventoryResponeType.ItemRemoved;
        }

        return InventoryResponeType.Error;
    }

    public InventoryResponeType RemoveItem(IItem item)
    {
        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            if(inventory.GetItem(i) == item)
            {
                inventory.RemoveItem(i);
                OnItemRemovedDelegateEvent.Invoke(item, i);
                return InventoryResponeType.ItemRemoved;
            }
        }

        return InventoryResponeType.Error;
    }

    public IItem GetItem(int index)
    {
        return inventory.GetItem(index);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
}

public enum InventoryResponeType
{
    ItemAdded,
    ItemRemoved,
    InventoryIsFull,
    Error
}
