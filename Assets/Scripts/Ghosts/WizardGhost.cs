using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardGhost : Ghost
{
    [SerializeField]
    GameObject spell;

    Timer rechargeSpellTime;

    override protected void Awake()
    {
        base.Awake();
        lifePoints = 5.0f;
        strength = 1.0f;
        velocity = 0.25f;
        scorePoints = 150;
        rechargeSpellTime = gameObject.AddComponent<Timer>();
        rechargeSpellTime.Duration = 3f;
        rechargeSpellTime.Run();
    }

    override protected void Update()
    {
        base.Update();
        if (rechargeSpellTime.Finished && !reachedTarget)
        {
            ThrowSpell(spell);
        }
    }

    void ThrowSpell(GameObject spell)
    {
        anim.Play(GetAttack());
        rechargeSpellTime.Duration = 5f;
        rechargeSpellTime.Run();
        Vector3 spawnPosition = this.gameObject.transform.position;
        Quaternion spawnRotation = this.gameObject.transform.rotation;
        GameObject g = Instantiate(spell, spawnPosition, spawnRotation);
        SpriteRenderer spr = g.GetComponent<SpriteRenderer>();

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

        Spell sp = g.GetComponent<Spell>();
        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(i * sp.Velocity, j * sp.Velocity), ForceMode2D.Impulse);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy_spell"))
        {
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("collectable"))
        {
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }
    }

}
