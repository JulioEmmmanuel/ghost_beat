using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    float velocity;

    public void Move(Direction dir) {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        int i, j;
        if (dir == Direction.Down)
        {
            spr.sortingLayerName = "behindc";
            i = 0;
            j = 1;
        }
        else if (dir == Direction.Up)
        {
            spr.sortingLayerName = "abovec";
            i = 0;
            j = -1;
        }
        else if (dir == Direction.Right)
        {
            spr.sortingLayerName = "abovec";
            i = -1;
            j = 0;
        }
        else
        {
            spr.sortingLayerName = "abovec";
            i = 1;
            j = 0;
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(i * velocity, j * velocity), ForceMode2D.Impulse);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy_spell"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("spell"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("player_shield"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("collectable"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

    }

    virtual protected void Awake()
    {
        velocity = 2.0f;
    }

}
