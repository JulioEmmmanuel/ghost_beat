using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpell : Spell
{
    void Awake()
    {
        strength = 1f;
        velocity = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayableCharacter>().GetDamage(strength);
            Destroy(gameObject);
        } else if(collision.gameObject.tag == "player_shield")
         {
            collision.gameObject.GetComponent<Shield>().GetDamage(strength);
            Destroy(gameObject);
        }
    }
}
