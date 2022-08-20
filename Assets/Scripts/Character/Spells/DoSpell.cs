using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSpell : Spell
{

    // Start is called before the first frame update
    void Awake()
    {
        strength = 2.5f;
        velocity = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Ghost>().GetDamage(strength);
            Destroy(gameObject);
        } else if(collision.gameObject.tag == "enemy_spell")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

}
