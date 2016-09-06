using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private ConsumableType item;
    [SerializeField] private Agent inventoryCarrier;

    [Header("UI Dependecies")]
    [SerializeField] private Text itemCount;

    private Inventory inventoryComponent;

	private void Start ()
    {
        inventoryComponent = inventoryCarrier.RequestComponent<Inventory>();
	}

    private void Update ()
    {
        if (inventoryComponent == null) return;

        itemCount.text = inventoryComponent.GetItemStock(item).ToString();
	}
}