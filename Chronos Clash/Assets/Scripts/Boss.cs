using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float speed;
    [SerializeField] Slider healthBar;
    [HideInInspector] public int health = 100;
    public Transform[] targetPositions;
    Player player;
    [HideInInspector] public bool isFlipped = false;
    [SerializeField] GameObject blueOrbAttackPrefab;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] GameObject yellowOrbAttackPrefab;
    [SerializeField] GameObject redOrbAttackPrefab;
    public Transform redOrbTarget;

    bool once;
    Animator anim;

    [SerializeField] GameObject bOEA;
    [SerializeField] GameObject yOEA;
    [SerializeField] GameObject rOEA;

    public AudioClip blueOrbSound;
    public AudioClip redOrbSound;
    public AudioClip yellowOrbSound;
    public AudioClip screamSound;
    SFX sfx;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        sfx = FindObjectOfType<SFX>();
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
        if (health<=0)
        {
           gameObject.SetActive(false);
           BossProjectile[] remainingProjectiles = FindObjectsOfType<BossProjectile>();
            foreach(BossProjectile p in remainingProjectiles)
            {
                Destroy(p.gameObject);
            }
        }
        if (health<=50 && health>0)
        {
            if(!once)
            {
                sfx.PlayAnySound(screamSound);
                anim.SetTrigger("change");
                once = true;
                speed *= 2;
            }
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

    public void Attack3()
    {
        sfx.PlayAnySound(blueOrbSound);
        if(!once)
        {
            Instantiate(blueOrbAttackPrefab, pointA.position, Quaternion.identity);
            Instantiate(blueOrbAttackPrefab, pointB.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bOEA, pointA.position, Quaternion.identity);
            Instantiate(bOEA,pointB.position, Quaternion.identity);
        }
        
    }

    public void Attack2()
    {
        sfx.PlayAnySound(yellowOrbSound);
        if(!once)
        {
            StartCoroutine(InstantiateHourGlass());
        }
        else
        {
            StartCoroutine(InstantiateEHourGlass());
        }
    }

    IEnumerator InstantiateHourGlass()
    {
        for(int i = 0;i<4;i++)
        {
            Instantiate(yellowOrbAttackPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    IEnumerator InstantiateEHourGlass()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(yOEA, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    public void Attack1()
    {
        sfx.PlayAnySound(redOrbSound);
        if(!once)
        {
            Instantiate(redOrbAttackPrefab,transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(rOEA, transform.position, Quaternion.identity);
        }
    }
}
