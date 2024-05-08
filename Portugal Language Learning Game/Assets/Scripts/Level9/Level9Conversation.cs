using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level9Conversation : MonoBehaviour
{
    XmlReader reader = new XmlReader();

    public StringUIPrinter namePrinter;
    private TMP_Text dialogeueText; // Using TextMeshPro for dialogue text
    private ChoiceButtonHandler choiceButtons;
    public ChoiceButtonHandler Btn;
    public delegate void LockConvo(bool talks);
    public LockConvo ConvoLocker;
    public GameObject EndPanel;
    public GameObject FailedPanel;
    public GameObject victoryPanel;
    private bool isTalking;
    
    [SerializeField]
    private string file;    //xml file name
    [SerializeField]
    private string path;    //path through xml file
    [SerializeField]
    private int id;         //number that represents the relevant character script in the xml file character array
    [SerializeField]
    private string initialXmlTag;   //the part of the xml that is first shown to the player upon starting a conversation
    public GameObject NPCImage;
    private string text;
    private float typingSpeed = 0.02f; // Adjust typing speed here

    private Coroutine typingCoroutine; // Coroutine reference for typing animation

    public bool talk = true;


    //public Animator Level9NPC;
    

    private bool gameEnded = false;

    public Animator player;

    void Awake()
    {
        
        choiceButtons = GameObject.FindWithTag(Tags.canvasTag).GetComponent<ChoiceButtonHandler>();
        /*namePrinter = GameObject.FindWithTag(Tags.nameText).GetComponent<StringUIPrinter>();*/
        dialogeueText = GameObject.FindWithTag(Tags.dialogueText).GetComponentInChildren<TMP_Text>();// Using TextMeshPro for dialogue text

        EndPanel.SetActive(false);
        FailedPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void ConversationStart()
    {
        talk = true;

        NPCImage.SetActive(true);
        isTalking = true;
        if (ConvoLocker != null)
        {
            ConvoLocker(isTalking);
        }

        text = reader.ReadXml(file, path, "Name", id);
        //namePrinter.PrintToUI(text);

        // Clear existing text before typing new dialogue
        dialogeueText.text = "";
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogue_1(reader.ReadXml(file, path, initialXmlTag, id)));

    }

    IEnumerator TypeDialogue_1(string dialogue)
    {
        foreach (char letter in dialogue)
        {
            
            dialogeueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
        
        // After typing is complete, present UI button choices loaded from xml.
        GetChoices("/" + initialXmlTag);
    }
    IEnumerator TypeDialogue_2(string dialogue, string location)
    {
        foreach (char letter in dialogue)
        {

            
            dialogeueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
       
        // After typing is complete, present UI button choices loaded from XML.
        GetChoices("/" + location);
    }

    void GetChoices(string location)
    {
        talk = false;
        choiceButtons.GetChoices(reader.ReadSubnodes(file, path + location, id), reader.ChoiceAttributes);
        choiceButtons.PassChoice += ConversationUpdate;
    }

    void ConversationUpdate(string lineTree)
    {
        talk = true;

        choiceButtons.PassChoice -= ConversationUpdate;

        if (lineTree == "TreeQuit")
        {
            EndConversation();
        }
        else if (lineTree == "TreeSuccess")
        {
            SucessConversation();
        }
        else
        {
            

            // SManage.instance.IncreaseScore(1);
            // Print relevant data to screen depending on player's latest choice
            text = reader.ReadXml(file, path, lineTree, id);
            // Clear existing text before typing new dialogue
            dialogeueText.text = "";
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeDialogue_2(text, lineTree));


        }
    }

    void EndConversation()

    {
        FailedPanel.SetActive(true);
        talk = false;
        Btn.PlayerImage.SetActive(false);
        //namePrinter.PrintToUI("");
        dialogeueText.text = "";

        isTalking = false;

        if (ConvoLocker != null)
        {
            ConvoLocker(isTalking);
        }
        NPCImage.SetActive(false);
    }

    public void SucessConversation()
    {
        EndGameScore();
        player.gameObject.GetComponent<Animator>().enabled = false;
        isTalking = false;

        Btn.PlayerImage.SetActive(false);
        //namePrinter.PrintToUI("");
        dialogeueText.text = "";
        gameObject.SetActive(false);

        if (ConvoLocker != null)
        {
            ConvoLocker(isTalking);
        }
        NPCImage.SetActive(false);
    }

    private void EndGameScore()
    {
        if (SManage.instance.score < 5)
        {
            FailedPanel.SetActive(true);
        }
        else
        {
            var CurrentPlayer = GameObject.FindGameObjectWithTag("CurrentPlayer");
            if (CurrentPlayer != null)
            {
                if (SManage.instance.score == 5)
                {
                    victoryPanel.SetActive(true);
                    EndPanel.SetActive(true);
                    if (CurrentPlayer.GetComponent<CurrentPlayer>().Score == 8)
                    {
                        Debug.Log("Victory Card 9 and level 10 Unlocked ");
                        CurrentPlayer.GetComponent<CurrentPlayer>().Score = 9;
                        SManage.instance.StartCoroutine("SavePlayerScore");
                    }
                    else
                    {
                        Debug.Log("Victory Card 9 was already unlocked");
                    }
                }
            }

        }
    }
}
