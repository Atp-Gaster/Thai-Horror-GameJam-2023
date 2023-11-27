using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletbehavior1 : MonoBehaviour
{
    [HideInInspector] public DamageField damage;
    [SerializeField]public float speed;
    // Start is called before the first frame update
    void Start()
    {
        damage.Start();
     

    }
    public void setspeed(GameObject from,GameObject target) 
    {
       var mok= (Vector2)(target.transform.position - gameObject.transform.position);
        gameObject.GetComponent<Rigidbody2D>().velocity=mok.normalized*speed;
        Vector2 p = from.transform.position;
        Vector2 tag = target.transform.position;
        Vector2 d = tag - p;
        float angle = Mathf.Atan2(d.y, d.x) / Mathf.PI * 180;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        from.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    // Update is called once per frame
    void Update()
    {
        var deltatime = Time.deltaTime;
        damage.Update(deltatime);

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        var dam = damage.creaseHp(collision.gameObject);
        if (dam < 0)
        {
            //IDK nice effect here

            PartsBehaviour.playsound("light",0.5f);
        }
    }
    void dead()
    {
        // IDK nice effect to here;
        Destroy(gameObject);
    }

}
