using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject escapePanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapePanel.SetActive(true);
        }
    }
}
