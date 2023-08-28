using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    Player player;
    public float throwForce = 10.0f; // Force to throw the bomb
    public float explosionRadius = 5.0f; // Radius of the explosion
    public float explosionDelay = 2.0f; // Time in seconds before the bomb explodes


    public Vector3 throwDirection;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        if(Player.instace != null) transform.position = Player.instace.transform.position;
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        throwDirection = Vector3.right; // Adjust the throw direction as needed
    }

    private void Update()
    {
        if(Player.instace != null) throwDirection = Player.instace.bDir;
        rb.velocity = throwDirection * throwForce;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Object")))
        {
            ObjectPool.instance.ReturnGrenade(this);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            ObjectPool.instance.ReturnGrenade(this);
        }
    }
   
}
