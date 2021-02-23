using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "inventory/item")]
public class Items : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite image;
    public int StressRemoved;
    public float speedGiven;
    public float speedDuration;
    public float jumpBoostGiven;
    public int Price;
    public bool dashReset;
    public bool jumpGiven;
}
