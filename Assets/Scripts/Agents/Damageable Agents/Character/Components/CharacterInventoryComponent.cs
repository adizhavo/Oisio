using UnityEngine;
using System.Collections.Generic;

public class CharacterInventoryComponent : CharacterComponent
{
    private List<Slot> inventorySlots = new List<Slot>();

    public CharacterInventoryComponent(CharacterAgent agent) : base (agent) { }

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

    public int GetItemStock(ConsumableType requestedType)
    {
        foreach(Slot s in inventorySlots)
        {
            if (s.slotType.Equals(requestedType))
            {
                return s.StockItem;
            }
        }

        return -1;
    }

    #region CharacterComponent implementation

    public override void FrameFeed() { }

    #endregion
}

public class Slot
{
    public ConsumableType slotType;
    public int maxSize;

    private int stockItem;
    public int StockItem
    {
        set
        {
            stockItem = value;
            stockItem = Mathf.Clamp(stockItem, 0, maxSize);
        }
        get
        {
            return stockItem;
        }
    }

    public Slot(ConsumableType slotType, int maxSize)
    {
        this.slotType = slotType;
        this.maxSize = maxSize;
        StockItem = 0;
    }

    public void AddItem()
    {
        StockItem ++;
    }

    public void UseItem()
    {
        StockItem --;
    }

    public bool HasItems()
    {
        return stockItem > 0;
    }
}
