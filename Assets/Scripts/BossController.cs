using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
    public healthBar bosshb;

    public Transform player;
    public bool isFlipped = false;

    public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
        bosshb.SetMaxHealth(maxHealth);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            GetComponent<BossWeapon>().attackOffset.x *= -1;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            GetComponent<BossWeapon>().attackOffset.x *= 1;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        bosshb.SetHealth(currentHealth);

        if(currentHealth <= (maxHealth / 2))
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Boss died");
        }
    }

    public void Die()
    {
        // Death Animation
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Debug.Log("Boss died");
    }
}
