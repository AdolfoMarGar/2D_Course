using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeAI : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private RaycastHit2D raycastHit;

    public float movHor = 0f;
    public float speed = 5f;

    public bool isGroundFloor = true;
    public bool isGroundedFront = false;

    public LayerMask groundLayer;
    public LayerMask flipWall;
    public float frontGroundRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float floorCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGiven = 50;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // raycastHit = GetComponent<Raycast>();
    }


    // Update is called once per frame
    void Update()
    {
        // Si se detecta una colisión a la izquierda o a la derecha, llama a flip
        if (CheckCollision(Vector3.left) || CheckCollision(Vector3.right))
        {
            movHor *= -1;
        }

    }
    
    private bool CheckCollision(Vector3 direction)
    {
        // Método para verificar colisiones en una dirección específica
        return Physics2D.CircleCast(transform.position, frontDist, direction, frontGroundRayDist, flipWall);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(movHor * speed, rigidBody.velocity.y);

    }
    private void LateUpdate()
    {

    }
    private void getKilled()
    {
        gameObject.SetActive(false);
    }
}
