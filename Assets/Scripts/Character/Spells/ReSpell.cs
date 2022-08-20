using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpell : Spell
{
    [SerializeField]
    GameObject shield;

    void Awake()
    {
        strength = 0f;
        velocity = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag=="enemy_spell")
        {
            Vector3 spawnPosition = this.gameObject.transform.position;
            Quaternion spawnRotation = this.gameObject.transform.rotation;
            GameObject g = Instantiate(shield, spawnPosition, spawnRotation);
            SpriteRenderer spr = g.GetComponent<SpriteRenderer>();
            ReShield resh = g.GetComponent<ReShield>();

            if (dir == Direction.Up)
            {
                spr.sortingLayerName = "behindc";
                resh.Dir = Direction.Up;
            }
            else if (dir == Direction.Down)
            {
                spr.sortingLayerName = "abovec";
                resh.Dir = Direction.Down;
            }
            else if (dir == Direction.Left)
            {
                spr.sortingLayerName = "abovec";
                resh.Dir = Direction.Left;

            }
            else
            {
                spr.sortingLayerName = "abovec";
                resh.Dir = Direction.Right;
            }

            spr.sprite = resh.GetSprite();
            
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("spell"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
            }

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("player_shield"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
            }

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("collectable"))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
            }

            Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), g.GetComponent<Collider2D>());

            if (collision.gameObject.tag == "enemy_spell")
            {
                Destroy(collision.gameObject);
            }
            
            Destroy(gameObject);

        }
    }
    
}
