using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject slotPrefab;
    public Transform ContenedorSlot;

    [Header("Inventario")]
    public List<ItemInventario> items;

    private void Start()
    {
        ActualizarInventarioUI();
    }

    public void ActualizarInventarioUI()
    {
        foreach(Transform hijo in ContenedorSlot)
        {
            Destroy(hijo.gameObject);
        }

        foreach(ItemInventario item in items)
        {
            GameObject SlotNuevo = Instantiate(slotPrefab, ContenedorSlot);

            Image icono = SlotNuevo.transform.Find("Icono").GetComponent<Image>();
            icono.sprite = item.icono;

            Text cantidad = slotPrefab.transform.Find("Cantidad").GetComponent<Text>();
            cantidad.text = item .cantidad.ToString();
        }
    }
}
