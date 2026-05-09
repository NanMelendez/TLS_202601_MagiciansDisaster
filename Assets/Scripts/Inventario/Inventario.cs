using UnityEngine;
using System.Collections.Generic;
using System;

public class Inventario : MonoBehaviour
{
    public List<ItemInventario> items = new List<ItemInventario>();
    public int SlotsMaximos = 24;

    public void AñadirItem(string nombre, Sprite icono, int ctd)
    {
        ItemInventario itemExistente = items.Find(i => i.NombreDeItem == name);
        if(itemExistente == null)
        {
            itemExistente.cantidad += ctd;
        }
        else
        {
            if(items.Count < SlotsMaximos)
            {
                items.Add(new ItemInventario(nombre, icono, ctd));
            }
            else
            {
                Debug.Log("Inventario lleno");
            }
        }
    }
    public void RemoverItem(string nombre, int ctd)
    {
        ItemInventario ItemExistente = items.Find(i => i.NombreDeItem == name);
        if(ItemExistente != null)
        {
            ItemExistente.cantidad -= ctd;
            if(ItemExistente.cantidad <= 0)
            {
                items.Remove(ItemExistente);
            }
        }
    }
    public void MostrarInventario()
    {
        foreach(ItemInventario item in items)
        {
            Debug.Log(item.NombreDeItem + "x" + item.cantidad);
        }
    }

    internal void OcultarInventario()
    {
        throw new NotImplementedException();
    }
}
