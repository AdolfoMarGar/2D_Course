using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeAI : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private RaycastHit2D hitEnemy;

    public float movHor = 0f;
    public float speed = 5f;

    public Transform sideTransform;
    public Transform bottomTransform;

    public bool isGroundFloor = true;
    public bool isGroundedFront = false;

    public LayerMask groundLayer;
    public LayerMask enemyWall;
    public float frontGroundRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGiven = 50;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        // Si se detecta una colisión a la izquierda o a la derecha, llama a flip
        if (CheckWallCollision(Vector3.left) || CheckWallCollision(Vector3.right))
        {
            movHor *= -1;
        }
        if (CheckWallCollision(Vector3.down))
        {
            DestroyObject();
        }
        CheckCollisionWithEnemy();

        rigidBody.velocity = new Vector2(movHor * speed, rigidBody.velocity.y);
    }

    private void DestroyObject()
    {
        // Método para destruir el objeto
        Destroy(gameObject);
    }

    private bool CheckWallCollision(Vector3 direction)
    {
        // Método para verificar colisiones en una dirección específica usando Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, frontGroundRayDist, enemyWall);

        return hit.collider != null;
    }

    private void CheckCollisionWithEnemy()
    {
        Vector3 raycastOrigin = new(transform.position.x + (movHor * sideTransform.localPosition.x), transform.position.y, transform.position.z); // Originar el Raycast desde la posición actual del objeto

        Vector2 raycastDirection = new(movHor, 0);

        // Realiza un Raycast desde la posición actual del objeto en la dirección de movimiento horizontal (movHor)
        hitEnemy = Physics2D.Raycast(raycastOrigin, raycastDirection, frontDist);

        // Comprueba si el Raycast ha golpeado algo
        if (hitEnemy.collider != null)
        {
            if (hitEnemy.collider.gameObject != gameObject)
            {
                if (hitEnemy.transform.CompareTag("Enemy"))
                {
                    movHor *= -1; // Invierte la dirección del movimiento horizontal
                }
            }
        }
    }

}
