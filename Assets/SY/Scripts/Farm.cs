using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Farm")]
public class Farm : ScriptableObject
{
    [SerializeField]
    private string farmName;
    public string FarmName { get { return farmName; } }
    [SerializeField]
    private Sprite farmImage;
    public Sprite FarmImage { get { return farmImage; } }
    [SerializeField]
    private List<Upgrade> upgrades;
    public List<Upgrade> Upgrades { get { return upgrades; } }
}
