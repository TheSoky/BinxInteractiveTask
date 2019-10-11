using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	[SerializeField]
	[Tooltip("Transform of a grid holding the inventory items")]
	private Transform _itemParent;

	[SerializeField]
	[Tooltip("Prefab of the item slot")]
	private GameObject _itemSlot;

	[SerializeField]
	[Tooltip("Maximum number of slots")]
	private int _maxAmountOfSlots = 36;

	[SerializeField]
	[Tooltip("List of all items")]
	private List<ItemSO> _allItems = new List<ItemSO>();

	private List<GameObject> _inventorySlots = new List<GameObject>();

}
