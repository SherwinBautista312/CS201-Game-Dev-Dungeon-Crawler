using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //Upgrade weapon
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing weapon
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        //checking if we can swing again
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if last swing is greater than cooldown then we can swing again
            if (Time.time - lastSwing > cooldown)
            {
                //changing last swing to new time
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if(coll.name == "Player")//ignore player hit by own weapon
            {
                return;
            }

            //create a new damage object, then sent it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
        
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}
