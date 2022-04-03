using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // load scene by name
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    // quit or stop playing
    public void Quit()
    {
        Application.Quit();

        // stop play if in editor
        // #if UNITY_EDITOR strips this EDITOR ONLY code from builds
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
