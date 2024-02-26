using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public string Placedobjecttag;
    public string word1;
    public string word;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Get the tag of the dropped object
            string droppedObjectTag = eventData.pointerDrag.tag;
            
            if(droppedObjectTag == "open A")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1+ 'á' + word;
            }
            if(droppedObjectTag == "open O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'ó' + word;
            }
            if(droppedObjectTag == "closed A")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'a' + word;
            }
            if (droppedObjectTag == "closed O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'o' + word;
            }
            if (droppedObjectTag == "closed E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'e' + word;
            }
            if (droppedObjectTag == "open E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'é' + word;
            }
            if (droppedObjectTag == "middle E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'e' + word;
            }
            if (droppedObjectTag == "i")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'i' + word;
            }
            if (droppedObjectTag == "middle O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'o' + word;
            }
            if (droppedObjectTag == "u")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = word1 + 'u' + word;
            }


            //eventData.pointerDrag.GetComponentInChildren<Button>().onClick.RemoveAllListeners(); 
            eventData.pointerDrag.GetComponentInChildren<Button>().interactable = false; 
            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);

            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
