using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioWave : Spell
{
    [SerializeField]
    List<Sprite> sprites;


    public void setSprite()
    {
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = sprites[(int) dir];
    }

    void Awake()
    {
        strength = 0f;
        velocity = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayableCharacter pc = collision.gameObject.GetComponent<PlayableCharacter>();
            pc.EffectTimer.Drop();
            pc.EffectTimer.Duration = 5f;
            pc.EffectTimer.Run();
            pc.invertDirections();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "player_shield")
        {
            Destroy(gameObject);
        }
    }
}
