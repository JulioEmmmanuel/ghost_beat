using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{

    private float maxLifePoints;
    private float lifePoints;
    private Direction dir;

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private List<string> idleAnimations;

    [SerializeField]
    private List<string> attackAnimations;

    [SerializeField]
    private List<GameObject> spells;
    [SerializeField]
    private List<float> spellsCoolDowns;
    private List<Timer> spellTimers;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private List<AudioClip> audios;

    private SpriteRenderer sp;
    private Animator anim;
    private AudioSource audsou;

    private Timer animTimer;
    private Timer effectTimer;

    private Vector3 color;
    private Vector3 dxColor;

    private Direction[] directions = new Direction[4];

    [SerializeField]
    private HealthBar healthBar;


    public Timer EffectTimer
    {
        get
        {
            return effectTimer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        audsou = gameObject.GetComponent<AudioSource>();
        sp.sprite = sprites[0];
        anim.Play(idleAnimations[0]);

        animTimer = gameObject.AddComponent<Timer>();
        animTimer.Duration = 0.01f;
        animTimer.Run();

        maxLifePoints = 20;
        lifePoints = maxLifePoints;

        color = new Vector3(255, 255, 255);
        dxColor = (new Vector3((200 - 255), (50 - 255), (50 - 255))) / lifePoints;

        directions[0] = Direction.Up;
        directions[1] = Direction.Right;
        directions[2] = Direction.Down;
        directions[3] = Direction.Left;

        effectTimer = gameObject.AddComponent<Timer>();
        effectTimer.Duration = 0.001f;
        effectTimer.Run();

        healthBar.SetMaxHealth(lifePoints);

        spellTimers = new List<Timer>();
        foreach(GameObject go in spells)
        {
            Timer t = gameObject.AddComponent<Timer>();
            t.Duration = 0.001f;
            t.Run();
            spellTimers.Add(t);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (animTimer.Finished)
        {
            anim.Play(idleAnimations[(int)dir]);

            if (Input.anyKey)
            {
                changeDirection();
            }

                if (Input.GetKeyDown(KeyCode.Space))
            {
                attack(0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                attack(1);
            }
        }

        if (effectTimer.Finished)
        {
            sp.color = new Color(color.x / 255, color.y / 255, color.z / 255);
            resetDirections();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }

    }


    void changeDirection()
    {
        if (Input.GetKey("up"))
        {
            dir = directions[0];

        }
        else if (Input.GetKey("right"))
        {
            dir = directions[1];
        }
        else if (Input.GetKey("down"))
        {
            dir = directions[2];

        }
        else if (Input.GetKey("left"))
        {
            dir = directions[3];

        }

        sp.sprite = sprites[(int)dir];
        anim.Play(idleAnimations[(int)dir]);
    }

    public void invertDirections()
    {
        sp.color = new Color(200.0f/255, 20.0f/255, 200.0f/255);
        System.Random r = new System.Random();
        directions = directions.OrderBy(x => r.Next()).ToArray();
    }

    public void resetDirections()
    {
        directions[0] = Direction.Up;
        directions[1] = Direction.Right;
        directions[2] = Direction.Down;
        directions[3] = Direction.Left;
    }

   
    private void attack(int i)
    {

        anim.Play(attackAnimations[(int)dir]);
        animTimer.Duration = 0.35f;
        animTimer.Run();

        if (spellTimers[i].Finished)
        {
            spellTimers[i].Duration = spellsCoolDowns[i];
            spellTimers[i].Run();

            GameObject spell = spells[i];


            Vector3 spawnPosition = this.gameObject.transform.position;
            Quaternion spawnRotation = this.gameObject.transform.rotation;
            GameObject g = Instantiate(spell, spawnPosition, spawnRotation);
            SpriteRenderer spr = g.GetComponent<SpriteRenderer>();
            Spell sp = g.GetComponent<Spell>();

            int x, y;
            if (dir == Direction.Up)
            {
                sp.Dir = Direction.Up;
                spr.sortingLayerName = "behindc";
                x = 0;
                y = 1;
            }
            else if (dir == Direction.Down)
            {
                sp.Dir = Direction.Down;
                spr.sortingLayerName = "abovec";
                x = 0;
                y = -1;
            }
            else if (dir == Direction.Left)
            {
                sp.Dir = Direction.Left;
                spr.sortingLayerName = "abovec";
                x = -1;
                y = 0;
            }
            else
            {
                sp.Dir = Direction.Right;
                spr.sortingLayerName = "abovec";
                x = 1;
                y = 0;
            }

            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(x * sp.Velocity, y * sp.Velocity), ForceMode2D.Impulse);

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
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), go.GetComponent<Collider2D>());
            }

            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    public void GetDamage(float forceAttack)
    {
        lifePoints -= forceAttack;
        color += dxColor;
        audsou.PlayOneShot(audios[0]);
        healthBar.SetHealth(lifePoints);

        if (effectTimer.Finished)
        {
            sp.color = new Color(color.x / 255, color.y / 255, color.z / 255);
        }

        if (lifePoints <= 0)
        {
            audsou.PlayOneShot(audios[1]);
            Die();
        }
    }

    public void AddLife(float life)
    {
        audsou.PlayOneShot(audios[2]);
        if (lifePoints + life <= maxLifePoints)
        {
            lifePoints += life;
            color -= dxColor;
            color -= dxColor;
            color -= dxColor;
            healthBar.SetHealth(lifePoints);
        }
    }

    void Die()
    {
        Vector3 spawnPosition = this.gameObject.transform.position;
        Quaternion spawnRotation = this.gameObject.transform.rotation;
        Instantiate(explosion, spawnPosition, spawnRotation);
        MenuManager.GoToMenu(MenuName.GameOver);
        Destroy(gameObject);

    }


}
