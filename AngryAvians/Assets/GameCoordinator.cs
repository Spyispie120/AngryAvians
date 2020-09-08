using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoordinator : MonoBehaviour
{
    public static GameCoordinator Instance;
    public GameObject EnemyList;
    public List<Oink> enemies;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        foreach(Transform t in EnemyList.transform)
        {
            enemies.Add(t.GetComponent<Oink>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count <= 0)
        {
            Debug.Log("next level");
        }
    }

    public void RemoveFromList(Oink o)
    {
        enemies.Remove(o);
    }
}
