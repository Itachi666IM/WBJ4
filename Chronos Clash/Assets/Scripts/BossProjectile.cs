using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public Player player;
    public string projectileColour;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = FindObjectOfType<Player>();
        Destroy(gameObject, 3f);
    }
}
