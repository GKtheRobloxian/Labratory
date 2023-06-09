using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float rotate;
    public float votate;
    public float ytate;
    public float xtate;
    public float ztate;
    public float crouchSharpness;
    Vector3 startPos;

    float fireRate = 0.49f;
    float fire;
    public Quaternion proj;
    public Rigidbody laser;

    // Start is called before the first frame update
    void Start()
    {
        fire = fireRate;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xtate = transform.localEulerAngles.x;
        ytate = transform.localEulerAngles.y;
        ztate = transform.localEulerAngles.z;
        CameraRotation();
        CameraMovement();
        Firing();
    }

    void Firing()
    {
        fire -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fire < 0)
        {
            Rigidbody bullet;
            bullet = Instantiate(laser, transform.position, transform.rotation) as Rigidbody;
            bullet.AddRelativeForce(Vector3.forward * 100f, ForceMode.Impulse);
            fire = fireRate;
        }
    }

    void CameraRotation()
    {
        rotate = Input.GetAxis("Mouse X");
        votate = Input.GetAxis("Mouse Y");
        transform.RotateAround(transform.position, -Vector3.up, rotate * -720 * Time.deltaTime);
        transform.Rotate(-Vector3.right, votate * 720 * Time.deltaTime);
    }

    void CameraMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y-(player.transform.localScale.y/4f), 0.05f), player.transform.position.z);
        }
        else
        {
            transform.position = player.transform.position;
        }
    }
}
