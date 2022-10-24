using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    public enum ItemType { Slime,Crop,Gem}
    [SerializeField]
    private ItemType _type;
    public ItemType type { get { return _type; }}
    [SerializeField]
    private string _itemName;
    public string itemName { get { return _itemName; } }
    [SerializeField]
    private Sprite _itemSprite;
    public Sprite itemSprite { get { return _itemSprite; }}
    [SerializeField]
    private Mesh _itemMesh;
    public Mesh itemMesh { get { return _itemMesh; }}
    [SerializeField]
    private Material _itemMaterial;
    public Material itemMaterilal { get { return _itemMaterial; } }

}
