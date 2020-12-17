using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas shop;
    public List<Item> items;

    Character self;

    void Start()
    {
        items = new List<Item>();
        self = GetComponent<Character>();
        if (!self)
            throw new System.NullReferenceException(name + " does not have a character");
    }

    /// <summary>
    /// Flip flops the showing and hiding of the shop.
    /// </summary>
    public void ShowHide()
    {
        if (shop)
            shop.gameObject.SetActive(!shop.isActiveAndEnabled);
    }

    /// <param name="b">Whether to show or hide the shop.</param>
    public void ShowHide(bool b)
    {
        if (shop)   //  If there is a shop.
            shop.gameObject.SetActive(b);
        else
            if (self)
                throw new System.NullReferenceException(typeof(Character) + ": " + self._name + " does not have a " + typeof(Canvas));
            else
                throw new System.NullReferenceException(name + " does not have a " + typeof(Canvas));
    }

    public void PrintItems()
    {
        try
        {
            Item d = items[0];
        }
        catch (System.ArgumentOutOfRangeException)
        {
            Debug.LogError(self._name + " has no items.");
            return;
        }
        Debug.Log("Here are " + self._name + "'s items: ");
        for (int k = 0; k < items.Count; k++)
            Debug.Log(k + ": " + items[k]._name);
    }

    /// <param name="i">The item to search for.</param>
    /// <returns>If this character has Item i.</returns>
    public bool HasItem(Item i)
    {
        for (int k = 0; k < items.Count; k++)
            if (i._name.Equals(items[k]._name))
                return true;
        return false;
    }
}