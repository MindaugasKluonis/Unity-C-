using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int ItemID { set; get; }
    Sprite ItemSprite { set; get; }
    string ItemName { set; get; }
    string ItemDescription { set; get; }
    IItem ItemTemplate { set; get; }
}
