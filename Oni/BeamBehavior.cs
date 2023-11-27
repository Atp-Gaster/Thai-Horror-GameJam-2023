using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBehavior : MonoBehaviour
{
    [HideInInspector] public DamageField damage;
    [HideInInspector] public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        damage.Start();
        if (parent != null)
        {
            var tag = parent.transform.position;
            transform.position = tag;
        }

    }
    public void setdirection(GameObject from,GameObject target)
    {
        Vector2 p = from.transform.position;
        Vector2 tag = target.transform.position;
        Vector2 d = tag - p;
        float angle = Mathf.Atan2(d.y, d.x) / Mathf.PI * 180;
        gameObject.transform.rotation = Quaternion.Euler(0, 0,angle);
        from.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    // Update is called once per frame
    void Update()
    {
        var deltatime = Time.deltaTime;
        damage.Update(deltatime);

        gameObject.transform.localScale += new Vector3(0, -1, 0) * deltatime/damage.ENDS;

        var sp = gameObject.GetComponent<SpriteRenderer>();
            var col = sp.color;
            col.a -= deltatime / damage.ENDS;
            sp.color = col;
        

        {
            //IDK nice effect here
        }
        if (parent != null)
        {
            var tag = parent.transform.position;
            transform.position = tag;
        }
        else
        {
            dead();
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
        }
    }
    void dead()
    {
        // IDK nice effect to here;
        Destroy(gameObject);
    }

}
