using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour
{

    Transform player;
    public GameObject enemyFollowing;
    public float ytate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemyFollowing.transform.position;
        transform.LookAt(player);
        ytate = transform.localEulerAngles.y;
    }
}
