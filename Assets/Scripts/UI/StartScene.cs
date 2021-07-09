using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public string sceneName;
    Button playButton;
    // Start is called before the first frame update
    void Awake()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
    // Update is called once per frame

