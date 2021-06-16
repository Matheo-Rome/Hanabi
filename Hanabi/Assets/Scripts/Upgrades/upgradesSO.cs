﻿using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "inventory/upgrade")]
public class upgradesSO : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite image;
    public float campFireUpgrade;
    public int maxCoinsSaved;
    public float blurReduction;
    public int stressIncrease;
    public int givenObjectLevel;
    public int coinDropUpgrade;
}