using UnityEngine.SceneManagement; 
using UnityEngine;

public class levelchanger : MonoBehaviour
{

    public Animator animator;
    private int levelToLoad;

    // Update is called once per frame (CAN BE UPDATED)
    void Update()
    {
   
        if (Input.GetMouseButton(0))
        {
            FadetoNextLevel();
        }

    }

    public void FadetoNextLevel ()
    {

        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);

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

