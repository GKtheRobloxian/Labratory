using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Horizontal;
    public float Vertical;
    public float rotate;
    public float votate;
    float jumpAmount = 2;
    float jump;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = jumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        rotate = Input.GetAxis("Mouse X");
        votate = Input.GetAxis("Mouse Y");
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Vertical);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Horizontal);
        transform.RotateAround(transform.position, -Vector3.up, rotate * -5);
        transform.RotateAround(transform.right, -Vector3.zero, votate * 5);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump <= 0)
            {
                return;
            }
            else
            {
                rb.AddForce(Vector3.up * 250f);
                jump -= 1;
            }
        }
        if (rb.velocity.y == 0)
        {
            jump = jumpAmount;
        }
    }
}
