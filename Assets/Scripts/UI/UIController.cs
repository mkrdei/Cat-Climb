using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas levelPassedCanvas;
    // Start is called before the first frame update
    void Start()
    {
        levelPassedCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelPassedCanvas()
    {
        levelPassedCanvas.enabled = true;
    }
}
