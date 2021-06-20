using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomItemSelect : MonoBehaviour
{
    [SerializeField] private GameObject khune;
    [SerializeField] private GameObject updraft;
    [SerializeField] private GameObject dwich;

    public int spawnChance;
    // Start is called before the first frame update
    void Start()
    {
        var rd = new Random();
        var canSpawn = rd.Next(spawnChance);
        if (canSpawn != 0) return;
        var rdNum = rd.Next(3);
        switch (rdNum)
        {
            case 0 :
                khune.SetActive(true);
                break;
            case 1 :
                updraft.SetActive(true);
                break;
            case 2 :
                dwich.SetActive(true);
                break;
        }
    }
}
