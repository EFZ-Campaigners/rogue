using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData", order = 0)]
public class UnitData : ScriptableObject
{
    public new string name;
    public string description;

    public int cost;
    public int ATK;
    public int HP;
    public int QK;
    public int step;
    public int range;

}
