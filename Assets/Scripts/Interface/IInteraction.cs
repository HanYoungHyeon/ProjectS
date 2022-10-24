using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    public Rigidbody rigi { get; set; }
    public Item.ItemType type { get;}
    public void MoveStop();

}
