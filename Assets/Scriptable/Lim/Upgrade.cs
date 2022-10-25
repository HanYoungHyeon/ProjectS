using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField]
    private string upgradeName;
    public string UpgradeName { get { return upgradeName; } }
    [SerializeField,TextArea(3,5)]
    private string upgradeInfo;
    public string UpgradeInfo { get { return upgradeInfo; } }
    [SerializeField]
    private Sprite upgradeImage;
    public Sprite UpgradeImage { get { return upgradeImage; } }
    [SerializeField]
    private int requireCoin;
    public int RequireCoin { get { return requireCoin; } }
    

}
