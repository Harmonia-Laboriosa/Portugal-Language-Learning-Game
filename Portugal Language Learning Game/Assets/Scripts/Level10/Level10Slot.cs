using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Level10Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public string Placedobjecttag;
    public Level10Managers level10Manager;
    int mark = 1;
    //public string tag;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            level10Manager = FindObjectOfType<Level10Managers>();
            // Get the tag of the dropped object
            string droppedObjectTag = eventData.pointerDrag.tag;

            // Print the tag of the dropped object
            Debug.Log("Tag of dropped object: " + droppedObjectTag);
            if(eventData.pointerDrag.tag==Placedobjecttag)
            {
                level10Manager.IncScore(mark);
                //SManage.instance.IncreaseScore(1);
            }
            // Move the dropped object to the slot
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            
            level10Manager.Next(mark);
            //eventData.pointerDrag.gameObject.SetActive(false);
        }
    }

}
