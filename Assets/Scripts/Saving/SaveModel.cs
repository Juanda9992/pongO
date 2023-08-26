using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveModel", menuName = "Save data/SaveModel", order = 0)]
public class SaveModel : ScriptableObject 
{
    public int matchesWin;
    public int matchesLoss;
    public Vector3 color;

    public List<bool> colorUnlocked = new List<bool>();

}