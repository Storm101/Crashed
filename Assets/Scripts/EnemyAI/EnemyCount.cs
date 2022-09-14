using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    void Update()
    {
        GameManager.Instance.EnemiesLeft++;
    }
}
