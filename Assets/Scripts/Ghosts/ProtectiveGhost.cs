using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectiveGhost : Ghost
{
    protected Shield shield;
    protected float pointsToNext;

    [SerializeField]
    private GameObject replace;

    override public Sprite GetSprite()
    {
        int direction = (int)dir;
        int state = shield.CurrentState;
        return sprites[(int)shield.Endurance * direction + state];
    }

    override public string GetIdle()
    {
        int direction = (int)dir;
        int state = shield.CurrentState;
        return idleAnimations[(int)shield.Endurance * direction + state];
    }

    override public string GetAttack()
    {
        int direction = (int)dir;
        int state = shield.CurrentState;
        return attackAnimations[(int)shield.Endurance * direction + state];
    }


    override public void GetDamage(float forceAttack)
    {
        pointsToNext -= forceAttack;
        audsou.PlayOneShot(audios[0]);
        if (pointsToNext <= 0)
        {
            shield.advanceState();
            if (shield.Destroyed)
            {
                GameObject enemy = Instantiate(replace);
                Ghost g = enemy.GetComponent<Ghost>();
                g.Appear(transform.position, dir);
                Destroy(gameObject);
            }
            else
            {
                pointsToNext = shield.getBreakpoint();
                spr.sprite = GetSprite();
                if (reachedTarget)
                {
                    anim.Play(GetAttack());
                }
                else
                {
                    anim.Play(GetIdle());
                }
            }
        }
    }
}
