using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{

    public int damage;
    public int healOnHit;
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
            EnemyController controller = other.GetComponent<EnemyController>();
            controller.Damaged(actualDamage);
            Destroy(gameObject);
            if(Vector3.Distance(player.transform.position, other.transform.position) <= controller.bloodRange)
            {
                control.Bloodshed(healOnHit);
            }
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
