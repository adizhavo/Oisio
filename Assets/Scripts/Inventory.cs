using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<Slot> inventorySlots = new List<Slot>();

    public void AddSlot(Slot itemSlot)
    {
        inventorySlots.Add(itemSlot);
    }

    public void HasItems(CollectableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType) && s.HasItems())
                return true;
        }

        return false;
    }

    public void AddItem(CollectableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType))
            {
                s.AddItem();
                return;
            }
        }
    }

    public void UseItem(CollectableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType) && s.HasItems())
            {
                s.UseItem();
                return;
            }
        }
    }
}

public struct Slot
{
    public CollectableType slotType;
    public int maxSize;

    private int stockItem;

    public Slot(CollectableType slotType, int maxSize)
    {
        this.slotType = slotType;
        this.maxSize = maxSize;
        stockItem = 0;
    }

    public void SetStockItems(int items)
    {
        stockItem = items;
        stockItem = Mathf.Clamp(stockItem, 0, maxSize);
    }

    public void AddItem()
    {
        if (stockItem < maxSize)
            stockItem ++;
    }

    public void UseItem()
    {
        if (stockItem > 0)
            stockItem --;
    }

    public void HasItems()
    {
        return stockItem > 0;
    }
}
