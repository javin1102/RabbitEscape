using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void Play()
    {
        AudioManager.instance.PlayS("button");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        AudioManager.instance.PlayS("button");
        Application.Quit();
    }
}
