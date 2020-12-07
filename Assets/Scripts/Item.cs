using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI CostBox;

    public string _name;
    public int price;

    [Header("Attributes")]
    public float _health;
    public float _damage;

    void Start()
    {
        CostBox.text = price.ToString();
    }

    public int SellPrice()
    {
        return Mathf.FloorToInt(price * .75f);
    }
}