using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Map GameObjects for UnlockIfCleared()
    [SerializeField] GameObject buttonB;
    [SerializeField] GameObject buttonC;
    [SerializeField] GameObject buttonOne;
    [SerializeField] GameObject buttonTwo;
    [SerializeField] GameObject stars5;
    [SerializeField] GameObject buttonThree;
    [SerializeField] GameObject starThreeYes;
    [SerializeField] GameObject buttonThats;
    [SerializeField] GameObject starThatsYes;
    [SerializeField] GameObject buttonHow;
    [SerializeField] GameObject starHowGreenYes;
    [SerializeField] GameObject starHowYellowYes;
    [SerializeField] GameObject starHowRedYes;
    [SerializeField] GameObject buttonEasy;
    [SerializeField] GameObject starEasyGreenYes;
    [SerializeField] GameObject starEasyYellowYes;
    [SerializeField] GameObject starEasyRedYes;
    [SerializeField] GameObject buttonLove;
    [SerializeField] GameObject starLoveGreenYes;
    [SerializeField] GameObject starLoveYellowYes;
    [SerializeField] GameObject starLoveRedYes;
    [SerializeField] GameObject buttonCan;
    [SerializeField] GameObject starCanGreenYes;
    [SerializeField] GameObject starCanYellowYes;
    [SerializeField] GameObject starCanRedYes;
    [SerializeField] GameObject buttonBe;
    [SerializeField] GameObject starBeGreenYes;
    [SerializeField] GameObject starBeYellowYes;
    [SerializeField] GameObject starBeRedYes;
    [SerializeField] GameObject newLevelText;
    [SerializeField] GameObject buttonWinter;

    // Variables for UnlockLetterIfCleared
    [SerializeField] GameObject letterLevelSelect;
    private bool letterLevelSelectYes;

    // Start is called before the first frame update
    void Start()
    {
        // If levels and stars are cleared, set GameObjects active
        UnlockIfCleared("A", buttonB);
        UnlockIfCleared("B", buttonC);
        UnlockIfCleared("C", buttonOne);
        UnlockIfCleared("One", buttonTwo);
        UnlockIfCleared("Two", stars5);
        UnlockIfCleared("Two", buttonThree);
        UnlockIfCleared("ThreeStarGreen", starThreeYes);
        UnlockIfCleared("Three", buttonThats);
        UnlockIfCleared("ThatsStarRed", starThatsYes);
        UnlockIfCleared("Thats", buttonHow);
        UnlockIfCleared("HowStarGreen", starHowGreenYes);
        UnlockIfCleared("HowStarYellow", starHowYellowYes);
        UnlockIfCleared("HowStarRed", starHowRedYes);
        UnlockIfCleared("How", buttonEasy);
        UnlockIfCleared("EasyStarGreen", starEasyGreenYes);
        UnlockIfCleared("EasyStarYellow", starEasyYellowYes);
        UnlockIfCleared("EasyStarRed", starEasyRedYes);
        UnlockIfCleared("Easy", buttonLove);
        UnlockIfCleared("LoveStarGreen", starLoveGreenYes);
        UnlockIfCleared("LoveStarYellow", starLoveYellowYes);
        UnlockIfCleared("LoveStarRed", starLoveRedYes);
        UnlockIfCleared("Love", buttonCan);
        UnlockIfCleared("CanStarGreen", starCanGreenYes);
        UnlockIfCleared("CanStarYellow", starCanYellowYes);
        UnlockIfCleared("CanStarRed", starCanRedYes);
        UnlockIfCleared("Can", buttonBe);
        UnlockIfCleared("BeStarGreen", starBeGreenYes);
        UnlockIfCleared("BeStarYellow", starBeYellowYes);
        UnlockIfCleared("BeStarRed", starBeRedYes);
        UnlockIfCleared("Be", newLevelText);
        UnlockIfCleared("Be", buttonWinter);

        // If all stars are cleared, unlock Letter
        UnlockLetterIfCleared();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Load selected level on button press
    public void LoadLevel()
    {
        string selectedLevel = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(selectedLevel);
    }

    // If levelCleared has been cleared, unlock objectToUnlock 
    private void UnlockIfCleared(string levelCleared, GameObject objectToUnlock)
    {
        // Check if level has been cleared
        bool clear = (PlayerPrefs.GetInt(levelCleared) != 0);

        //Unlock objectToUnlock if level has been cleared
        if (clear)
        { objectToUnlock.SetActive(true); }
    }

    // If all stars are cleared, unlock Letter
    private void UnlockLetterIfCleared()
    {
        // If letter has been cleared, set it active
        letterLevelSelectYes = (PlayerPrefs.GetInt("LevelSelectLetter") != 0);
        if (letterLevelSelectYes)
        { letterLevelSelect.SetActive(true); }

        // If letter not clear, check if all stars collected
        else if (
            PlayerPrefs.GetInt("ThreeStarGreen") != 0 &&
            PlayerPrefs.GetInt("ThatsStarRed") != 0 &&
            PlayerPrefs.GetInt("HowStarGreen") != 0 &&
            PlayerPrefs.GetInt("HowStarYellow") != 0 &&
            PlayerPrefs.GetInt("HowStarRed") != 0 &&
            PlayerPrefs.GetInt("EasyStarGreen") != 0 &&
            PlayerPrefs.GetInt("EasyStarYellow") != 0 &&
            PlayerPrefs.GetInt("EasyStarRed") != 0 &&
            PlayerPrefs.GetInt("LoveStarGreen") != 0 &&
            PlayerPrefs.GetInt("LoveStarYellow") != 0 &&
            PlayerPrefs.GetInt("LoveStarRed") != 0 &&
            PlayerPrefs.GetInt("CanStarGreen") != 0 &&
            PlayerPrefs.GetInt("CanStarYellow") != 0 &&
            PlayerPrefs.GetInt("CanStarRed") != 0 &&
            PlayerPrefs.GetInt("BeStarGreen") != 0 &&
            PlayerPrefs.GetInt("BeStarYellow") != 0 &&
            PlayerPrefs.GetInt("BeStarRed") != 0
        )
        // If all stars, unlock Letter
        {
            PlayerPrefs.SetInt("LevelSelectLetter", 1);
            letterLevelSelect.SetActive(true);
        }
    }

    // If do not pressed, destroy all progress and reload
    public void DoNotPress()
    {
        // Destroy all progress
        PlayerPrefs.DeleteAll();

        // Load Level Select
        SceneManager.LoadScene("LevelSelect");
    }
}
