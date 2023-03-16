using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float initialSpawnRate = 2.0f;
    float spawnRate;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = initialSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scamp = new Vector3(Random.Range(-50, 50), 4f, Random.Range(-50, 50));
        transform.position = scamp;
        spawnRate -= Time.deltaTime;
        if (spawnRate <= 0)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            spawnRate = initialSpawnRate;
        }
    }
}
