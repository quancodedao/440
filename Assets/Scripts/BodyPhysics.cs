using UnityEngine;
using System.Collections.Generic;
public class BodyPhysics : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [SerializeField] private bool useGravity = true;
    [SerializeField] private Vector3 gravity = new Vector3(0, -9.81f, 0);

    private Vector3 velocity = Vector3.zero;
    private Vector3 force = Vector3.zero;

    public void AddForce(Vector3 inForce)
    {
        force += inForce;
    }
    private void Update()
    {
        if (useGravity)
        {
            ApplyGravity();
        }

        ApplyForces();

        transform.position += velocity * Time.deltaTime;

        velocity = Vector3.zero;
    }

    public void ApplyForces()
    {
        velocity = force * (Time.deltaTime / mass);
    }

    private void ApplyGravity()
    {
        velocity += gravity * (Time.deltaTime / mass);
    }
}