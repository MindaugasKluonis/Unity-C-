using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IItem", menuName = "Items/IItem")]
public class ItemScriptable : ScriptableObject, IItem
{
    [SerializeField]
    private int itemID;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDescription;

    public int ItemID { get => itemID; set => itemID = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public IItem ItemTemplate { get; set; }


}




//  if (GameItems[0] is Weapon)
//((Weapon) GameItems[0]).Attack();