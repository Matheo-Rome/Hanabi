using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //Attribut du pannel de dialogue
    
    //Nom du PNJ
    public string name;

    //Phrase du pnj
    //TextArea permet d'avoir des grands carrés pour mettre plus d'une phrase dans les dialogues
    [TextArea(3, 10)]
    public string[] sentences;
}
