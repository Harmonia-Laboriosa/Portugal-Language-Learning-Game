using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level3DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;                 //reference to Canvas
    private RectTransform rectTransform;   //reference to RectTransform
    private CanvasGroup canvasGroup;       //reference to CanvasGroup
    private Vector2 originalPosition;      //Reference to the position it is at the start of the scene
    public bool isDraggable = true;        //isDraggable bool to check whether you can drag the gameobject 
    public bool isPlaceCorrect = false;        //isDraggable bool to check whether you can drag the gameobject 


    [SerializeField]
    private string tag;                    //tag of the gameobject where object will be placed


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;

        // Debug.Log("On Begin Dragging");
        //make the draggable object little transparent
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;

        //Debug.Log("Dragging");
        //You can drag the object and it should drag with pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if isDraggable is false than return the function and cannot drag
        if (!isDraggable) return;

        //Debug.Log("On End Dragging");

        //make the draggable object opaque again
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (eventData.hovered.Count > 0)
        {
            foreach (GameObject hoveredObject in eventData.hovered)
            {

                if (hoveredObject.CompareTag(tag))
                {
                    // If dropped onto a slot, snap to its position
                    rectTransform.anchoredPosition = hoveredObject.GetComponent<RectTransform>().anchoredPosition;
                    RectTransform hoveredRectTransform = hoveredObject.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = hoveredRectTransform.sizeDelta;
                    //make is draggable true

                    //isDraggable = false;
                    /*
                    string slotTag = GetComponent<ItemSlot>().Placedobjecttag;
                    if(slotTag==gameObject.tag)
                    {
                        isPlaceCorrect = true;  
                    }
                    */
                    //Debug.Log("1");
                    // If dropped onto a slot, get the Placedobjecttag from the ItemSlot component
                    Level3ItemSlot itemSlot = hoveredObject.GetComponent<Level3ItemSlot>();
                    if (itemSlot != null)
                    {
                        // Get the Placedobjecttag from the ItemSlot component
                        string placed_ObjectTag = itemSlot.Placedobjecttag;

                        // Do something with the placedObjectTag
                        Debug.Log("Placed object tag: " + placed_ObjectTag);

                        // Check if the placed object tag matches the tag of this object
                        if (string.Equals(placed_ObjectTag, gameObject.tag))
                        {
                            isPlaceCorrect = true;
                            Debug.Log("Object placed correctly!");
                        }
                        else
                        {
                            isPlaceCorrect = false;
                            Debug.Log("Object placed incorrectly!");
                        }
                    }

                    isDraggable = false;
                    StartCoroutine(DeactivateSelf());
                    return; // Exit the loop once a valid slot is found
                }


            }
        }

        // If not dropped onto a slot, return to original position
        rectTransform.anchoredPosition = originalPosition;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

}
