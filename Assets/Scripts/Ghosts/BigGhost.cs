using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGhost : Ghost
{

    [SerializeField]
    GameObject replace;

    protected override void Awake()
    {
        base.Awake();
        lifePoints = 20.0f;
        strength = 1.0f;
        velocity = 0.05f;
        scorePoints = 300;
    }

    protected override void Die()
    {
        base.Die();
        Vector3 spawnPosition = this.gameObject.transform.position;
        GameObject enemy = Instantiate(replace);
        Ghost g = enemy.GetComponent<Ghost>();
        g.Appear(spawnPosition, dir);

        if(dir == Direction.Up)
        {
            spawnPosition.y += 0.5f;
        }else if(dir == Direction.Down)
        {
            spawnPosition.y -= 0.5f;
        } else if(dir == Direction.Right)
        {
            spawnPosition.x += 0.5f;
        } else
        {
            spawnPosition.x -= 0.5f;
        }

        GameObject enemy2 = Instantiate(replace);
        Ghost g2 = enemy2.GetComponent<Ghost>();
        g2.Appear(spawnPosition, dir);
    }

}
