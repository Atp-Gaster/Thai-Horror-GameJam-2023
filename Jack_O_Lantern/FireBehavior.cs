using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    [SerializeField] protected DamageField damage;
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
        var dam=damage.creaseHp(collision.gameObject);
        if (dam < 0)
        {
        //IDK nice sound here
        }
    }
    void dead() 
    {
        // IDK nice effect to here;
        Destroy(gameObject);
    }

}
