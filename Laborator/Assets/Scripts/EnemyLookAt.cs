using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour
{

    Transform player;
    public Quaternion ytate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        RotationStorage();
    }

    void Look()
    {
        transform.LookAt(player);
    }

    void RotationStorage()
    {
        ytate = transform.rotation;
    }
}
