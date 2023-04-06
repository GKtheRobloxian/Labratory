using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int health;
    GameObject players;
    Transform player;
    public int blood;
    public float bloodRange;
    public float lungeRange;
    public float speed;
    public GameObject lookAt;
    EnemyLookAt enemy;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player");
        player = players.transform;
        enemy = lookAt.GetComponent<EnemyLookAt>();

    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
        Chase();
    }

    public void Damaged(int damage)
    {
        health -= damage;
    }

    void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            if (Vector3.Distance(transform.position, player.transform.position) < bloodRange)
            {
                PlayerController plau = players.GetComponent<PlayerController>();
                plau.Bloodshed(blood);
            }
        }
    }

    void Chase()
    {
        transform.rotation = Quaternion.Euler(new Vector3 (0, enemy.ytate.y, 0));
        Debug.Log(transform.rotation.y);
        if (Vector3.Distance(transform.position, player.transform.position) > lungeRange)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
