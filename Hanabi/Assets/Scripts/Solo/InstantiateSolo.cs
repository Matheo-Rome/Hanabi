using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSolo : MonoBehaviour
{
    public int P1;
    public int P2;
    [SerializeField] private GameObject Classic;
    [SerializeField] private GameObject Bouncy;
    [SerializeField] private GameObject Light;
    
    [SerializeField] private GameObject Classic2;
    [SerializeField] private GameObject Bouncy2;
    [SerializeField] private GameObject Light2;

    [SerializeField] private GameObject sp1;
    [SerializeField] private GameObject sp2;

    [SerializeField] private GameObject theEndIsNear;
    [SerializeField] private GameObject TheOtherOne;
    

    private void Awake()
    {
        if (P1== 1)
             Instantiate(Classic,sp1.transform.position,Quaternion.identity);
        else if (P1 == 2)
            Instantiate(Bouncy,sp1.transform.position,Quaternion.identity);
        else
            Instantiate(Light,sp1.transform.position,Quaternion.identity);

        if (P2 == 1)
             Instantiate(Classic2,sp2.transform.position,Quaternion.identity);
        else if (P2 == 2)
             Instantiate(Bouncy2,sp2.transform.position,Quaternion.identity);
        else
            Instantiate(Light2,sp2.transform.position,Quaternion.identity);
        
        Destroy(TheOtherOne);
        Destroy(theEndIsNear);
        Destroy(sp1);
        Destroy(sp2);
        Destroy(this);
    }
}
