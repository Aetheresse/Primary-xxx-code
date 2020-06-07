using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public GameObject fhiteffect;
    public LayerMask enLayers;
    public Transform fPoint;
    public float fRange;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject , 1f);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(fPoint.position, fRange, enLayers);


        // Cause Damage
       foreach (Collider2D enemy in hitEnemy)
       {
            enemy.GetComponent<Enemyone>().TakeDamage(5);
            GameObject effect = Instantiate(fhiteffect, transform.position, Quaternion.identity);

           Destroy(effect, .5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (fPoint == null)
           return;

       Gizmos.DrawWireSphere(fPoint.position, fRange);



    }

}
