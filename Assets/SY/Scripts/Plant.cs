using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Plant")]
public class Plant : Item
{
    [SerializeField]
    private float _growTime;
    public float growTime { get { return _growTime; } private set { } }
    [SerializeField]
    private float _discardTime;
    public float discardTime { get { return _discardTime; } private set { } }
    [SerializeField,Range(0,8)]
    private int _plantCount;
    public int plantCount { get { return _plantCount; } private set { } }
    [SerializeField]
    private Crop fruit;
    public Crop Fruit { get { return fruit; } private set { } }
}
