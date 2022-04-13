using UnityEngine;

[CreateAssetMenu(fileName = "Treasure", menuName = "Treasure", order = 0)]
public class Treasure : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;

    public int cost;
    public bool passive;

}
