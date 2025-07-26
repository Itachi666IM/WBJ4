using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float speed;
    [SerializeField] Slider healthBar;
    private int health = 100;
    public Transform[] targetPositions;
    Player player;
    [HideInInspector] public bool isFlipped = false;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damageAmount)
    {
        if(health>0)
        {
            health -= damageAmount;
            healthBar.value = health;
        }
    }

    private void Update()
    {
        if (health<0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public Transform NewTargetPos()
    {
        return targetPositions[Random.Range(0, targetPositions.Length)];
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x> player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180f, 0);
            isFlipped = false;
        }
        else if(transform.position.x<player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180f, 0);
            isFlipped = true;
        }
    }
}
