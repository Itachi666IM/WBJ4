using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject escapePanel;
    public Transform[] checkpoints;
    Transform currentCheckPoint;
    int index = 0;
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentCheckPoint = checkpoints[index];
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapePanel.SetActive(true);
        }
    }

    public void MoveToNextCheckpoint()
    {
        if (index+1 < checkpoints.Length)
        {
            index++;
            currentCheckPoint = checkpoints[index];
        }
    }

    public void SpawnPlayerAtCheckPoint()
    {
        if (player.isDead)
        {
            player.transform.position = currentCheckPoint.position;
            player.isDead = false;
            player.anim.SetTrigger("checkpoint");
        }
    }
}
