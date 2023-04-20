using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    public int health;
    GameObject players;
    Transform player;
    public int blood;
    public float bloodRange;
    public float lungeRange;
    public float lungeForce;
    public float speed;
    public GameObject lookAt;
    EnemyLookAt enemy;
    float chaseTimer = 1.5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player");
        player = players.transform;
        enemy = lookAt.GetComponent<EnemyLookAt>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
        FilthyControls();
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
            Destroy(lookAt);
            if (Vector3.Distance(transform.position, player.transform.position) < bloodRange)
            {
                PlayerController plau = players.GetComponent<PlayerController>();
                plau.Bloodshed(blood);
            }
        }
    }

    void FilthyControls()
    {
        chaseTimer -= Time.deltaTime;
        Quaternion rotate = Quaternion.Euler(new Vector3(0, enemy.ytate, 0));
        transform.rotation = rotate;
        if (Vector3.Distance(transform.position, player.transform.position) > lungeRange)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= lungeRange)
        {
            if (chaseTimer < 0)
            {
                StartCoroutine(LeapCoroutine());
                chaseTimer = 1.5f;
            }
        }
    }

    IEnumerator LeapCoroutine()
    {
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        rb.AddRelativeForce(Vector3.forward * lungeForce, ForceMode.Impulse);
    }
}
