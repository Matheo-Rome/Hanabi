using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED_platform : MonoBehaviour
{
    public void Desactive()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
