using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class IngridentSlot : MonoBehaviour, IDropHandler

{
    [SerializeField]
    public string Placedobjecttag;
    public bool canbePlaced;
    

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Get the tag of the dropped object
            string droppedObjectTag = eventData.pointerDrag.tag;

            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);

            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text="";
        }
        
    }
}
