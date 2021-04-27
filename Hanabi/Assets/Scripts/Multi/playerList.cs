using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerList : ScriptableObject
{
    [SerializeField] public List<GameObject> players = new List<GameObject>();
}
