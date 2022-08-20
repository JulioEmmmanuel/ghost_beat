using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReShield : Shield
{
    [SerializeField]
    List<Sprite> sprites;

    private float pointsToNext;
    private Direction dir;

    private List<Ghost> attackers;

    public Direction Dir
    {
        get
        {
            return dir;
        }
        set
        {
            dir = value;
        }
    }

    void Awake()
    {
        breakpoints = new List<float>();
        attackers = new List<Ghost>();
        endurance = 3;
        breakpoints.Add(1);
        breakpoints.Add(1);
        breakpoints.Add(1);
    }

    public Sprite GetSprite()
    {
        int direction = (int)dir;
        int state = currentState;
        return sprites[3 * direction + state];
    }

    override public void addAttacker(GameObject go)
    {
        attackers.Add(go.GetComponent<Ghost>());
    }


    override public void GetDamage(float forceAttack)
    {
        pointsToNext -= forceAttack;
        if (pointsToNext <= 0)
        {
            advanceState();
            if (destroyed)
            {
                foreach(Ghost attacker in attackers)
                {
                    if (attacker != null)
                    {
                        attacker.ReachedTarget = false;
                        attacker.Anim.Play(attacker.GetIdle());
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                pointsToNext = getBreakpoint();
                gameObject.GetComponent<SpriteRenderer>().sprite = GetSprite();
            }
        }
    }
}
