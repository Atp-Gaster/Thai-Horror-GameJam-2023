using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField]protected GameObject PlayerObject=null;
    [SerializeField] protected GameObject EnemyObject=null;

    [HideInInspector] public PlayerBehavior player;
    [HideInInspector] public PlayerBehavior enemy;
    // Start is called before the first frame update
    void Start()
    {
        setPAndE();
    }
    /// <summary>
    /// set Player and enemy from inspector
    /// </summary>
    public void setPAndE() 
    {
        if (PlayerObject != null)
        {
            player = PlayerObject.GetComponent<PlayerBehavior>();
        }
        if (EnemyObject != null)
        {
            enemy = EnemyObject.GetComponent<PlayerBehavior>();
        }
        player.target = enemy;
        enemy.target = player;
    }
    bool player_win { get { return enemy.is_dead; } }
    bool enemy_win { get { return player.is_dead; } }

    bool battle_end { get { return player_win || enemy_win; } }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            SceneManager.LoadScene("Battle Scene");
        }
      //  Debug.Log(player.core.sumOfHp+" VS "+enemy.core.sumOfHp);
    }
}
