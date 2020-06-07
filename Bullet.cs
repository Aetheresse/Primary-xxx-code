using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hiteffect;
    public LayerMask playerLayers;
    public Transform bPoint;
    public float bRange;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(bPoint.position, bRange, playerLayers);


        // Cause Damage
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController2D>().TakeDamage(5);
            GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
            
            Destroy(effect, 5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (bPoint == null)
            return;

        Gizmos.DrawWireSphere(bPoint.position, bRange);



    }

}
