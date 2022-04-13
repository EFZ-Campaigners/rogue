using UnityEngine;

[CreateAssetMenu(fileName = "Piece", menuName = "Piece", order = 0)]
public class Piece : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;

    public int cost;
    public int AT;
    public int HP;
    public int QK;
    public int step;
    public int range;
}