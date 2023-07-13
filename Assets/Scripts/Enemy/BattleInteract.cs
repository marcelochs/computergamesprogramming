using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            List<MonsterBase> enemyMonster = transform.parent.GetComponent<MonsterPossesion>().allMonster;
            
            List<MonsterBase> ownMonster =  other.gameObject.GetComponent<MonsterPossesion>().allMonster;
            
            MonsterManager.MonsterInstance.setMonster(ownMonster, enemyMonster);
            SceneManager.LoadScene(1);
        }
    }
}
