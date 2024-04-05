using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ChoiceButtonHandler : MonoBehaviour
{

    private string[] attributes;
    private string[] playerChoices;
    private GameObject[] currentButtons;
    public GameObject PlayerImage; // Removed unnecessary 'public void GameObject'
    public TMP_Text Player_Dialogue; // Changed to 'public' instead of 'private'

    [SerializeField]
    private GameObject choiceButtonPrefab;

    [SerializeField]
    private float xPos;
    const float yPosOffset = 115f; // Removed 'const' as it's unnecessary for a serialized field

    public delegate void ChoicePasser(string lineTree);
    public ChoicePasser PassChoice;

    [SerializeField]
    private string subTree;

    public Level2Animation_NPC1 npcanim;
    public Level2Animation_NPC2 npcanim2;
    public bool talk = false;

    public GameObject dialogue;

    public void Start()
    {
        //npcanim = FindObjectOfType<Level2Animation_NPC1>();
        //npcanim2 = FindObjectOfType<Level2Animation_NPC2>();
    }

    public void GetChoices(string[] receivedChoices, string[] choiceAttributes)
    {
        attributes = choiceAttributes;
        playerChoices = receivedChoices;
        CreateChoiceButtons();
    }

    void CreateChoiceButtons()
    {
        PlayerImage.SetActive(true);
        currentButtons = new GameObject[playerChoices.Length];
        Player_Dialogue.text = " ";
        if (!talk)
        {
            if (dialogue != null)
            {
                if (dialogue.activeSelf) // Use activeSelf instead of active
                {
                    if (npcanim2 != null)
                    {
                        npcanim2.idleTalk();
                    }
                }
                else
                {
                    if (npcanim != null)
                    {
                        npcanim.idleTalk();
                    }
                }
            }
            talk = true;
        }

        int i = 0;
        float offsetCounter = -275;

        foreach (string s in playerChoices)
        {
            GameObject choiceButtonObj = Instantiate(choiceButtonPrefab, Vector3.zero, Quaternion.identity);

            choiceButtonObj.name = "ChoiceButton: " + i;

            choiceButtonObj.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

            Button choiceButton = choiceButtonObj.GetComponent<Button>();
            choiceButton.GetComponentInChildren<TMP_Text>().text = playerChoices[i];

            Vector2 pos = Vector2.zero;
            pos.y = offsetCounter;
            pos.x = xPos;

            choiceButton.GetComponent<RectTransform>().anchoredPosition = pos;

            string att = attributes[i];
            choiceButton.onClick.AddListener(() => clickAction(att));

            offsetCounter -= yPosOffset;

            currentButtons[i] = choiceButtonObj;

            i++;
        }
    }

    void clickAction(string attribute)
    {
       // PlayerImage.SetActive(false);

        string buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;

        Debug.Log("Clicked button text: " + buttonText);
        Player_Dialogue.text = buttonText;

        for (int i = 0; i < currentButtons.Length; i++)
        {
            Destroy(currentButtons[i]);
        }
        if (talk)
        {
            if (dialogue != null)
            {
                if (dialogue.activeSelf) // Use activeSelf instead of active
                {
                    npcanim2.idleNotTalk();
                }
                else
                {
                    npcanim.idleNotTalk();
                }
            }
            talk = false;
        }
        currentButtons = null;

        if (PassChoice != null)
        {
            PassChoice(subTree + attribute);
        }
    }
}
