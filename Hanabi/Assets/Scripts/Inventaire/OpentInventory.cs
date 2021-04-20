using UnityEngine;

public class OpentInventory : MonoBehaviour
{
    public GameObject inventoryGO;
    void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            inventoryGO.SetActive(!inventoryGO.activeSelf);
        }
    }
}
