using UnityEngine.UI;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public int NombreDePièce;

    public static inventory instance;
    public Text compteurdecoinstext;

    private void Awake()
    {
        // Il faut qu'il n'y ai qu'un seul et unique inventaire
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance d'inventaire dans la scène");
            return;
        }
        
        instance = this;
    }

    public void Addcoins(int pièce)
    {
        NombreDePièce += pièce;
        compteurdecoinstext.text = NombreDePièce.ToString();
    }
    
}
