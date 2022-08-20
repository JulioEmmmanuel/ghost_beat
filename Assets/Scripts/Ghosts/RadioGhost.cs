using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioGhost : Ghost
{
    [SerializeField]
    GameObject radioWave;

    Timer rechargeWaveTime;

    override protected void Awake()
    {
        base.Awake();
        lifePoints = 5.0f;
        strength = 1.0f;
        velocity = 0.25f;
        scorePoints = 250;
        rechargeWaveTime = gameObject.AddComponent<Timer>();
        rechargeWaveTime.Duration = 5f;
        rechargeWaveTime.Run();
    }

    override protected void Update()
    {
        base.Update();
        if (rechargeWaveTime.Finished && !reachedTarget)
        {
            ThrowWave(radioWave);
        }
    }

    void ThrowWave(GameObject wave)
    {
        anim.Play(GetAttack());
        rechargeWaveTime.Duration = 10f;
        rechargeWaveTime.Run();

        Vector3 spawnPosition = this.gameObject.transform.position;
        Quaternion spawnRotation = this.gameObject.transform.rotation;
        GameObject g = Instantiate(wave, spawnPosition, spawnRotation);
        SpriteRenderer spr = g.GetComponent<SpriteRenderer>();
        RadioWave sp = g.GetComponent<RadioWave>();


        int i, j;
        if (dir == Direction.Down)
        {
            spr.sortingLayerName = "behindc";
            sp.Dir = Direction.Up;
            i = 0;
            j = 1;
        }
        else if (dir == Direction.Up)
        {
            spr.sortingLayerName = "abovec";
            sp.Dir = Direction.Down;
            i = 0;
            j = -1;
        }
        else if (dir == Direction.Right)
        {
            spr.sortingLayerName = "abovec";
            sp.Dir = Direction.Left;
            i = -1;
            j = 0;
        }
        else
        {
            spr.sortingLayerName = "abovec";
            sp.Dir = Direction.Right;
            i = 1;
            j = 0;
        }

        sp.setSprite();

        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(i * sp.Velocity, j * sp.Velocity), ForceMode2D.Impulse);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy_spell"))
        {
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }
    }
}
