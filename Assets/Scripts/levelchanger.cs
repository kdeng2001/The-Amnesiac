using UnityEngine.SceneManagement; 
using UnityEngine;

public class levelchanger : MonoBehaviour
{

    public Animator animator;
    private int levelToLoad;

    public delegate void LevelTransition();
    public static LevelTransition levelTransition;

    private void OnEnable()
    {
        levelTransition += FadeToNextLevel;
    }
    private void OnDisable()
    {
        levelTransition -= FadeToNextLevel;
    }
    // Update is called once per frame (CAN BE UPDATED)
    //void Update()
    //{

    //    if (Input.GetMouseButton(0))
    //    {
    //        FadetoNextLevel();
    //    }

    //}

    public void FadeToNextLevel ()
    {

        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Fade_Out");
    }
    public void FadeToLevel (int levelIndex)
    {

        levelToLoad = levelIndex;
        animator.SetTrigger("Fade_Out");

    }

    public void OnFadeComplete ()
    {

        SceneManager.LoadScene(levelToLoad); 

    }
}

