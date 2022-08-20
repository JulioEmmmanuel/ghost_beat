using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    protected float lifePoints;
    protected float strength;
    protected float velocity;
    protected int scorePoints;


    protected Direction dir;

    [SerializeField]
    protected List<Sprite> sprites;

    [SerializeField]
    protected List<string> idleAnimations;

    [SerializeField]
    protected List<string> attackAnimations;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    protected List<AudioClip> audios;

    private Vector3 pos, vel;
    private float angVel;
    private bool isColliding;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected SpriteRenderer spr;
    protected AudioSource audsou;

    protected bool reachedTarget;
    protected bool firstCollision;
    private GameObject target;

    protected Timer rechargeTime;

    protected List<GameObject> collectables;

    public float Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
        }
    }

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

    public bool ReachedTarget
    {
        get
        {
            return reachedTarget;
        }
        set
        {
            reachedTarget = value;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }
    }

    virtual public Sprite GetSprite()
    {
        return sprites[(int)dir];
    }

    virtual public string GetIdle()
    {
        return idleAnimations[(int)dir];
    }

    virtual public string GetAttack()
    {
        return attackAnimations[(int)dir];
    }

    public void AddCollectable(GameObject go)
    {
        collectables.Add(go);
    }

    virtual protected void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        audsou = gameObject.GetComponent<AudioSource>();
        collectables = new List<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        rechargeTime = gameObject.AddComponent<Timer>();
        rechargeTime.Duration = 0.01f;
        rechargeTime.Run();
    }


    // Update is called once per frame
    virtual protected void Update()
    {
        if(rechargeTime.Finished && reachedTarget)
        {
            Attack(target);
        }
    }

    void FixedUpdate()
    {
        if (!isColliding)
        {
            pos = rb2d.position;
            vel = rb2d.velocity;
            angVel = rb2d.angularVelocity;
        }
    }

    void LateUpdate()
    {
        if (isColliding)
        {
            rb2d.position=pos;
            rb2d.velocity=vel;
            rb2d.angularVelocity=angVel;
            isColliding = false;
        }
    }

    public void Appear(Vector3 location, Direction direction)
    {
        gameObject.transform.position = location;

        Vector2[] directions = new Vector2[4];
        directions[0] = new Vector2(0, -1);
        directions[1] = new Vector2(-1, 0);
        directions[2] = new Vector2(0, 1);
        directions[3] = new Vector2(1, 0);

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spr = gameObject.GetComponent<SpriteRenderer>();

        dir = direction;
        rb2d.AddForce(directions[(int) direction] * velocity, ForceMode2D.Impulse);
        spr.sprite = GetSprite();
        anim.Play(GetIdle());

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("enemy_spell"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("collectable"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "spell")
        {
            isColliding = true;
        } else if (collision.collider.tag == "Player" || collision.collider.tag == "player_shield")
        {
            rb2d.velocity = Vector3.zero;
            rb2d.angularVelocity = 0;
            reachedTarget = true;
            target = collision.collider.gameObject;
            Attack(collision.collider.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "player_shield")
        {
            reachedTarget = false;
            Vector2[] directions = new Vector2[4];
            directions[0] = new Vector2(0, -1);
            directions[1] = new Vector2(-1, 0);
            directions[2] = new Vector2(0, 1);
            directions[3] = new Vector2(1, 0);

            rb2d.AddForce(directions[(int)dir] * velocity, ForceMode2D.Impulse);
            anim.Play(GetIdle());
        }
    }

    private void OnBecameInvisible()
    {
        Die();
    }

    void Attack(GameObject go)
    {
        if (reachedTarget == true && target != null)
        {
            anim.Play(GetAttack());
            if (go.tag == "Player")
            {
                go.GetComponent<PlayableCharacter>().GetDamage(strength);
            }
            else if (go.tag == "player_shield")
            {
                go.GetComponent<Shield>().GetDamage(strength);
                go.GetComponent<Shield>().addAttacker(gameObject);
            }
            rechargeTime.Duration = 5f;
            rechargeTime.Run();
        }
    }

    virtual public void GetDamage(float forceAttack)
    {
        lifePoints -= forceAttack;
        audsou.PlayOneShot(audios[0]);
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    virtual protected void Die()
    {
        Vector3 spawnPosition = this.gameObject.transform.position;
        Quaternion spawnRotation = this.gameObject.transform.rotation;
        Instantiate(explosion, spawnPosition, spawnRotation);
        foreach(GameObject go in collectables)
        {
            GameObject collectable = Instantiate(go, spawnPosition, spawnRotation);
            collectable.GetComponent<Collectable>().Move(dir);

        }

         ScoreManager.instance.UpdateScore(scorePoints);
    }



}
