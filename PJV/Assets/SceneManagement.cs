using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Start()
    {
        ResumeTime();
    }

    public void SceneSwitch(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
