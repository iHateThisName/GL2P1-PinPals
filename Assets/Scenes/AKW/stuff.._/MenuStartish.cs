using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartish : MonoBehaviour
{
    public void startish()
    {
        SceneManager.LoadScene("menuTest2");
    }

    public void optionish()
    {
        SceneManager.LoadScene("menuTest3");
    }

    public void backish()
    {
        SceneManager.LoadScene("menuTest1");
    }

    public void quitish()
    {
        Debug.Log("You can't quit, u got work to do");
        Application.Quit();
    }
}
