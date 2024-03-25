using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenuUI;
    private bool isPaused = false;

    public delegate void TogglePause();
    public static TogglePause togglePause;

    private void Awake()
    {
        pauseMenuUI = GameObject.Find("pauseMenuUI");
        pauseMenuUI.SetActive(false); Debug.Log("awake pausemenu");
        
    }

    private void OnEnable()
    {
        togglePause += HandlePause;
    }
    private void OnDisable()
    {
        togglePause -= HandlePause;
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (isPaused)
    //            Resume();
    //        else
    //            Pause();
    //    }
    //}

    public void HandlePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // This will pause the game
        isPaused = true;
    }

    public void LoadMenu() { Resume(); SceneManager.LoadScene(0); }
    public void LoadLevel1() { Resume(); SceneManager.LoadScene(2); }
    public void LoadLevel2() { Resume(); SceneManager.LoadScene(3); }
    public void QuitGame()
    {
        // You can add more logic here like confirmation dialog if needed
        Application.Quit();
    }
}