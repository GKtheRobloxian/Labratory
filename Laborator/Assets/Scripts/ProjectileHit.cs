using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{

    public int damage;
    int actualDamage;
    GameObject player;
    PlayerController control;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        control = player.GetComponent<PlayerController>();
        DashBoost();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController control = other.GetComponent<EnemyController>();
            control.Damaged(actualDamage);
            Destroy(gameObject);
        }
    }

    public void DashBoost()
    {
        if(control.dashBoost > 0)
        {
            actualDamage = damage * 2;
        }
        else
        {
            actualDamage = damage;
        }
    }

}
