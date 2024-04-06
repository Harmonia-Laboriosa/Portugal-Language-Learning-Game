
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class Conversation : MonoBehaviour
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
    private bool isTalking;
    public Slider healthSlider;
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

    public Level2Animation_NPC1 npcanim;
    public Level2Animation_NPC2 npcanim2;
    public bool talk = true;

    public Level2AudioManager audio;
    void Awake()
    {
        audio = FindObjectOfType<Level2AudioManager>();
        choiceButtons = GameObject.FindWithTag(Tags.canvasTag).GetComponent<ChoiceButtonHandler>();
        /*namePrinter = GameObject.FindWithTag(Tags.nameText).GetComponent<StringUIPrinter>();*/
        dialogeueText = GameObject.FindWithTag(Tags.dialogueText).GetComponentInChildren<TMP_Text>();// Using TextMeshPro for dialogue text
    }

    public void ConversationStart()
    {
        talk = true;
        if (talk && npcanim!=null && npcanim2!=null)
        {
            npcanim.idleTalk();
            npcanim2.idleTalk();
        }
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
            audio.playTypesound();
            dialogeueText.text += letter;
           
            yield return new WaitForSeconds(typingSpeed);
        }
        audio.stopTypesound();
        // After typing is complete, present UI button choices loaded from xml.
        GetChoices("/" + initialXmlTag);
    }
    IEnumerator TypeDialogue_2(string dialogue, string location)
    {
        foreach (char letter in dialogue)
        {
            audio.playTypesound();
            dialogeueText.text += letter;
           
            yield return new WaitForSeconds(typingSpeed);
        }
        audio.stopTypesound();
        // After typing is complete, present UI button choices loaded from XML.
        GetChoices("/" + location);
    }

    void GetChoices(string location)
    {
        talk = false;
        if(!talk && npcanim != null && npcanim2 != null)
        {
            npcanim.idleNotTalk();
            npcanim2.idleNotTalk();
        }
        choiceButtons.GetChoices(reader.ReadSubnodes(file, path + location, id), reader.ChoiceAttributes);
        choiceButtons.PassChoice += ConversationUpdate;
    }

    void ConversationUpdate(string lineTree)
    {
        talk=true;
        if(talk && npcanim != null && npcanim2 != null)
        {
            npcanim.idleTalk();
            npcanim2.idleTalk();
        }
        
        choiceButtons.PassChoice -= ConversationUpdate;

        if (lineTree == "TreeQuit")
        {
            EndConversation();
        }
        else if(lineTree == "TreeSuccess")
        {
            SucessConversation();
        }
        else
        {
            IncreaseSliderValue();
           
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
    void IncreaseSliderValue()
    {
        if (healthSlider != null)
        {
            healthSlider.value += 10;
        }
    }
    void EndConversation()
    {
        talk = false;
        if (!talk && npcanim != null && npcanim2 != null)
        {
            npcanim.idleNotTalk();
            npcanim2.idleNotTalk();
        }
        Btn.PlayerImage.SetActive(false);
        namePrinter.PrintToUI("");
        dialogeueText.text = "";

        isTalking = false;
        FailedPanel.SetActive(true);
        if (ConvoLocker != null)
        {
            ConvoLocker(isTalking);
        }
        NPCImage.SetActive(false);
    }

    public void SucessConversation()
    {
        isTalking = false;
        EndPanel.SetActive(true);
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
}