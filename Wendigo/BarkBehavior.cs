using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkBehavior : MonoBehaviour
{
    [SerializeField] protected float sizescale = 10;
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

    // Update is called once per frame
    void Update()
    {
        var deltatime = Time.deltaTime;
        damage.Update(deltatime);

        gameObject.transform.localScale += new Vector3(sizescale, sizescale, sizescale) * deltatime/damage.ENDS;

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
