using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeBehavior : MonoBehaviour
{
    [SerializeField] public DamageField damage;
    [SerializeField] public float speed ;
    // Start is called before the first frame update
    void Start()
    {
        damage.Start();
   
    }

    // Update is called once per frame
    void Update()
    {
        var deltatime = Time.deltaTime;
        damage.Update(deltatime);

        gameObject.transform.localScale += new Vector3(0, -3, 0) * deltatime/damage.ENDS;

        var sp = gameObject.GetComponent<SpriteRenderer>();
            var col = sp.color;
            col.a -= deltatime / damage.ENDS;
            sp.color = col;
        

        {
            //IDK nice effect here
        }
        if (damage.NoMoreTimes)
        {
            dead();
        }
    }
    public void setspeed(GameObject target)
    {
        var mok = (Vector2)(target.transform.position - gameObject.transform.position);
        Vector2 sp;
        if(mok.x>0)sp=new Vector2(speed,0);
        else sp = new Vector2(-speed, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity=sp;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var dam = damage.creaseHp(collision.gameObject);
        if (dam < 0)
        {
            //IDK nice effect here
        }
    }
    void dead()
    {
        // IDK nice effect to here;
        Destroy(gameObject);
    }

}
