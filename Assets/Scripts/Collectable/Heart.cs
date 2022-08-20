using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    private float life;

    override protected void Awake()
    {
        base.Awake();
        life = 3.0f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayableCharacter pc = collision.gameObject.GetComponent<PlayableCharacter>();
            pc.AddLife(life);
            Destroy(gameObject);
        }
    }
}
