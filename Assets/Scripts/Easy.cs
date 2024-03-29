using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Easy : MonoBehaviour
{
    // Map audio
    private AudioSource audioSource;
    [SerializeField] AudioClip[] audioGreen;
    [SerializeField] AudioClip[] audioYellow;
    [SerializeField] AudioClip[] audioRed;

    // Map buttons and their containers
    [SerializeField] GameObject[] buttonsGreen;
    [SerializeField] GameObject[] buttonsYellow;
    [SerializeField] GameObject[] buttonsRed;

    // Counter to track which button is next
    private int stepCounterGreen = 0;
    private int stepCounterYellow = 0;
    private int stepCounterRed = 0;

    // String to track script name for CompleteLevel function
    private string scriptName;

    // Variables for Bork() script
    private Bork bork;
    private TMP_Text m_TextComponent;
    private Transform m_Transform;
    private string buttonText;


    // Start is called before the first frame update
    void Start()
    {
        // Map audio source
        audioSource = GetComponent<AudioSource>();

        // Map script name
        scriptName = this.GetType().Name;

        // Find Bork script
        bork = GameObject.Find("buttons").GetComponent<Bork>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
        For each button:
        - Deactivate button
        - Play audio
        - Wait
        - Increment step counter
        - Set next button container active
    */

    public void ButtonsGreen()
    {
        // Play audio
        buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        bork.BorkWord(buttonText);

        // Deactivate button
        buttonsGreen[stepCounterGreen].GetComponent<Button>().interactable = false;

        StartCoroutine(IButtonsGreen());
    }

    IEnumerator IButtonsGreen()
    {
        {// Wait 1 second
            yield return new WaitForSeconds(0.5f);

            // Increment step counter
            stepCounterGreen++;

            // Set next button active
            if (stepCounterGreen < buttonsGreen.Length)
            {
                buttonsGreen[stepCounterGreen].SetActive(true);
                buttonsGreen[stepCounterGreen].GetComponent<Button>().interactable = true;
            }

            // If no more buttons, complete level
            else
            {
                // Award green star
                PlayerPrefs.SetInt(scriptName + "StarGreen", 1);

                CompleteLevel();
            }
        }
    }

    public void ButtonsYellow()
    {
        // Play audio
        buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        bork.BorkWord(buttonText);

        StartCoroutine(IButtonsYellow());
    }

    IEnumerator IButtonsYellow()
    {
        {
            // Deactivate button
            buttonsYellow[stepCounterYellow].GetComponent<Button>().interactable = false;

            // Wait 1 second
            yield return new WaitForSeconds(0.5f);

            // Increment step counter
            stepCounterYellow++;

            // Set next button active
            if (stepCounterYellow < buttonsYellow.Length)
            { buttonsYellow[stepCounterYellow].SetActive(true); }

            // If no more buttons, complete level
            else
            {
                // Award yellow star if victory conditions are met
                if (stepCounterGreen == 0 && stepCounterRed == 0)
                { PlayerPrefs.SetInt(scriptName + "StarYellow", 1); }

                CompleteLevel();
            }
        }
    }

    public void ButtonsRed()
    {
        // Play audio
        buttonText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        bork.BorkWord(buttonText);

        StartCoroutine(IButtonsRed());
    }

    IEnumerator IButtonsRed()
    {
        {
            // Deactivate button
            buttonsRed[stepCounterRed].GetComponent<Button>().interactable = false;

            // Wait 1 second
            yield return new WaitForSeconds(0.25f);

            // Increment step counter
            stepCounterRed++;

            // Set next button active
            if (stepCounterRed < buttonsRed.Length)
            { buttonsRed[stepCounterRed].SetActive(true); }

            // If no more buttons, complete level
            else
            {
                // Award red star
                PlayerPrefs.SetInt(scriptName + "StarRed", 1);

                CompleteLevel();
            }
        }
    }

    // Mark level complete and load Level Select
    public void CompleteLevel()
    {
        // Mark level as complete
        PlayerPrefs.SetInt(scriptName, 1);
        StartCoroutine(WaitQuit());
    }

    // Wait, then load Level Select
    IEnumerator WaitQuit()
    {
        //Wait 1 second
        yield return new WaitForSeconds(1);

        // Load level select
        SceneManager.LoadScene("LevelSelect");
    }
}
