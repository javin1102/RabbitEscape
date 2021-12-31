using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField]
    GameObject sfx_source;
    private void OnLevelWasLoaded(int level)
    { 
        if(level == 0) AudioManager.instance.PlayM("main_menu");
        else AudioManager.instance.PlayM("gameplay");
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(sfx_source);
        DontDestroyOnLoad(gameObject);
        AudioManager.instance.PlayM("main_menu");
    }

}
