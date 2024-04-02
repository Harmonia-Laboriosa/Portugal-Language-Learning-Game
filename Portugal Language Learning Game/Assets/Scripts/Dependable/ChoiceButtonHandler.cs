using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChoiceButtonHandler : MonoBehaviour {

    private string[] attributes;    //collection of attributes of the avalible choices. these are required for determining the next step in the conversation
    private string[] playerChoices; //collection of strings that represent the player response options
    private GameObject[] currentButtons; //collection of the buttons that will be put on screen

    [SerializeField] // assign a default UI button in inspector
    private GameObject choiceButtonPrefab;

    [SerializeField]
    private float xPos;
    const float yPosOffset = 110f;

    public delegate void ChoicePasser(string lineTree);
    public ChoicePasser PassChoice;

    [SerializeField]
    private string subTree; // this is the name tag of the next section of the xml that the conversation should go to

    public Level2Animation_NPC1 npcanim;
    public Level2Animation_NPC2 npcanim2;
    public bool talk=false;

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
        currentButtons = new GameObject[playerChoices.Length];
        if(!talk)
        {
            
            if(dialogue.active)
            {
                npcanim2.idleNotTalk();
            }
            else
            {
                npcanim.idleNotTalk();
            }
            
            talk = true;
        }
        
        int i = 0;
        float offsetCounter = 24;

        //for each string in response options array for the player
        foreach (string s in playerChoices)
        {
            //instantiate new UI button
            GameObject choiceButtonObj = (GameObject)Instantiate(choiceButtonPrefab, Vector3.zero, Quaternion.identity);

            choiceButtonObj.name = "ChoiceButton: " + i;

            //make button child of canvas
            choiceButtonObj.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);

            Button choiceButton = choiceButtonObj.GetComponent<Button>();
            //assign appropriate text to button
            choiceButton.GetComponentInChildren<TMP_Text>().text = playerChoices[i];


            Vector2 pos = Vector2.zero;
            pos.y = offsetCounter;
            pos.x = xPos;

            //set button position
            choiceButton.GetComponent<RectTransform>().anchoredPosition = pos;
            
            string att = attributes[i];
            //use lambda to detect what response was picked & what attribute it had
            choiceButton.onClick.AddListener(() => clickAction(att));

            offsetCounter -= yPosOffset;

            currentButtons[i] = choiceButtonObj;

            i++;
        }
    }

    void clickAction(string attribute)
    {
        //button clicked

        //destroy current buttons
        for (int i = 0; i < currentButtons.Length; i++)
        {
            Destroy(currentButtons[i]);
        }
        if (talk)
        {
            if (dialogue.active)
            {
                npcanim2.idleTalk();
            }
            else
            {
                npcanim.idleTalk();
            }
            talk = false;
        }
        //prepare all variable for next choice batch
        currentButtons = null;

        //tell the conversation update where the conversation is progressing
        //specify the next step in the path of the conversation
        if (PassChoice != null)
        {
            PassChoice(subTree + attribute);
        }
    }
}
