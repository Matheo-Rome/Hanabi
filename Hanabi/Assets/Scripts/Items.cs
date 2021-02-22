using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "inventory/item")]
public class Items : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite image;
    public int StressRemoved;
    public int speedGiven;
    public float speedDuration;
    public int jumpBoostGiven; //test hugo
}
