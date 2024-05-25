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

    public AudioSource dragSound;
    public AudioClip dragClip;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Get the tag of the dropped object
            string droppedObjectTag = eventData.pointerDrag.tag;
            
            if(droppedObjectTag == "open A")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'á' + word + "</i>";
            }
            if(droppedObjectTag == "open O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'ó' + word + "</i>";
            }
            if(droppedObjectTag == "closed A")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'a' + word + "</i>";
            }
            if (droppedObjectTag == "closed O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'o' + word + "</i>";
            }
            if (droppedObjectTag == "closed E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'e' + word + "</i>";
            }
            if (droppedObjectTag == "open E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'é' + word + "</i>";
            }
            if (droppedObjectTag == "middle E")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'e' + word + "</i>";
            }
            if (droppedObjectTag == "i")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'i' + word + "</i>";

            }
            if (droppedObjectTag == "middle O")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'o' + word + "</i>";
            }
            if (droppedObjectTag == "u")
            {
                eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text = "<i>" + word1 + 'u' + word + "</i>";
            }


            //eventData.pointerDrag.GetComponentInChildren<Button>().onClick.RemoveAllListeners(); 
            if(eventData.pointerDrag.GetComponentInChildren<Button>()!=null)
            {
                eventData.pointerDrag.GetComponentInChildren<Button>().interactable = false;
            }
            dragSound.PlayOneShot(dragClip);
            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);

            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
