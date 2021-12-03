using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float maxSpeed;
    public float acceleration;

    private Rigidbody2D rigidBody;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = target.transform.position - transform.position;
        rigidBody.velocity += direction * acceleration * Time.deltaTime;
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
        transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, direction));
    }
}