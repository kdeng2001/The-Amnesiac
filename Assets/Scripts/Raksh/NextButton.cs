using UnityEngine;
using UnityEngine.SceneManagement; 

public class NextButton : MonoBehaviour
{
    private int currentLevelIndex;

    void Start()
    {
        // Get the index of the current active scene
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Function to be called when the "Next" button is clicked
    public void OnNextButtonClicked()
    {
        // Load the next scene based on the current index
        SceneManager.LoadScene(currentLevelIndex + 1);
    }
}