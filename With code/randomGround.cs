using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGround : MonoBehaviour
{
    [SerializeField] protected float max = 20;
    [SerializeField] protected float d = 5;
    [SerializeField] protected float dsp = 1;
    // Start is called before the first frame update
    float sum = 0;
    void Start()
    {
        randomall(gameObject,d);
    }
    public void randomall(GameObject o,float d)
    {
        float Rand = Random.Range(-d, d);
        if (o.GetComponent<Rigidbody2D>() != null) 
        {
            float over = sum - max;
            float lower = sum + max;
            if (over > 0) Rand -= over;
            if (lower > 0) Rand -= over;
            sum += Rand;
            o.transform.parent.Rotate(new Vector3(0,0,Rand));
        }
        foreach (Transform n in o.transform)
        {
            randomall(n.gameObject,d);
        }
    }
    // Update is called once per frame
    void Update()
    {
        randomall(gameObject,d*Time.deltaTime);
    }
}
