using UnityEngine;

public class OpentInventory : MonoBehaviour
{
    public GameObject inventoryGO;
    void Update()
    {
        if (Input.GetButton("OpenInventory"))
        {
            inventoryGO.SetActive(!inventoryGO.activeSelf);
        }
    }
}
