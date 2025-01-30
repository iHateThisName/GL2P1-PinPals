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

    public void menuOne()
    {
        SceneManager.LoadScene("Menu1");
    }

    public void toBeMade()
    {
        SceneManager.LoadScene("TBM");
    }

    public void onOffline()
    {
        SceneManager.LoadScene("OnOffline");
    }

    public void levelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

}
