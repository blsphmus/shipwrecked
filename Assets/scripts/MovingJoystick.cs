using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingJoystick : MonoBehaviour
{
    public float dirX, dirY;
    public float speed;
    public Joystick joystick;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = joystick.Horizontal * speed;
        dirY = 0;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);
    }
}
