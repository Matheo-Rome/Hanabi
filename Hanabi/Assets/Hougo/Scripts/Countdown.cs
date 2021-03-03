using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;
    
    public float timeStart;
    public Text displayTime;
    
    // Start is called before the first frame update
    public void Start()
    {
        displayTime.text = "";
    }

    // Update is called once per frame
    public void Update()
    {
        if (inventory.instance.cooldown > Time.time)
        {
            if (timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                displayTime.text = Mathf.Round(timeStart).ToString();
            }

            else
            {
                displayTime.text = "Ready";
            }
        }
    }
}
