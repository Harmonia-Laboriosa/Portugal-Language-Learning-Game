using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Manage : MonoBehaviour
{
    public GameObject[] panels; // Array to store references to all question panels
    public GameObject endPanel; // Reference to the end panel

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate all question panels and end panel initially
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        endPanel.SetActive(false);

        // Activate the first question panel
        StartCoroutine("startLevel");
    }

    IEnumerator startLevel()
    {
        yield return new WaitForSeconds(5f);
        if (panels.Length > 0)
        {
            panels[0].SetActive(true);
        }
    }
}
