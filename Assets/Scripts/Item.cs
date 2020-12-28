using UnityEngine;
using TMPro;

[RequireComponent(typeof(OnMouseDrag))]
public class Item : MonoBehaviour
{
    public TextMeshProUGUI CostBox;

    public string _name;
    public int price;

    [Header("Attributes")]
    public int _health;
    public int _damage;
    public int armour;
    public int MR;
    public int AttackSpeed;

    void Start()
    {
        CostBox.text = price.ToString();
    }

    public int SellPrice()
    {
        return Mathf.FloorToInt(price * .75f);
    }

    public override string ToString()
    {
        return _name + "\n" + price + "\n" + (_health > 0 ? _health.ToString() + "\n" : "") + ( _damage > 0 ? _damage.ToString() + "\n" : "");
    }
}