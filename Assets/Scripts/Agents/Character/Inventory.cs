using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<Slot> inventorySlots = new List<Slot>();

    public void AddSlot(Slot itemSlot)
    {
        inventorySlots.Add(itemSlot);
    }

    public bool HasItem(ConsumableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType))
                return s.HasItems();
        }

        return false;
    }

    public void AddItem(ConsumableType requestedType)
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

    public void UseItem(ConsumableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType))
            {
                s.UseItem();
                return;
            }
        }
    }
}

public class Slot
{
    public ConsumableType slotType;
    public int maxSize;

    private int stockItem;

    public Slot(ConsumableType slotType, int maxSize)
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

    public bool HasItems()
    {
        return stockItem > 0;
    }
}
