using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoordinator : MonoBehaviour
{
    public static GameCoordinator Instance;
    public GameObject EnemyList;
    public List<Oink> enemies;
    public string nextlevel;

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
            if (nextlevel.Length > 0)
            {
                StartCoroutine(NextLevel());
            }
        }
    }

    public void RemoveFromList(Oink o)
    {
        enemies.Remove(o);
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextlevel);
    }
}
