using UnityEngine;
using UnityEngine.UI;

public class TreasureDisplay : MonoBehaviour
{
    public Treasure treasure;
    public Image artworkImage;
    public Text cost;

    private void Start()
    {
        artworkImage.sprite = treasure.artwork;
        cost.text = treasure.cost.ToString();
    }
}
