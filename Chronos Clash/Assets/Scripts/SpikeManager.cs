using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    [SerializeField] Transform[] spikeSpawnPoints;
    [SerializeField] GameObject spikePrefab;
    Vector3 offset = new Vector3(-0.018f, -1.342f,0f);
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartingAtRightTime), 0.1f);
    }


    IEnumerator SpawnSpike()
    {
        for(int i= 0;i<spikeSpawnPoints.Length;i++)
        {
            GameObject instantiatedSpike =  Instantiate(spikePrefab, spikeSpawnPoints[i].position + offset,Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Destroy(instantiatedSpike);

        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SpawnSpike());
    }

    private void StartingAtRightTime()
    {
        StartCoroutine (SpawnSpike());
    }
}
