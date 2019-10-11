using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/New Item", order = 1)]
public class ItemSO : ScriptableObject
{
	public Sprite itemIcon;
	public string itemName;
	public int stacksUpTo;

}
