using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoItemInScene : MonoBehaviour
{
    private TMP_Text _name;
    public Item item;




    private void Start()
    {
        _name = GetComponentInChildren<TMP_Text>();
        _name.text = item.name;
    }
}
