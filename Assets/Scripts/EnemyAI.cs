using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyAI : MonoBehaviour
{
    Enemy enemy;

    UnityEvent [] unityevent;

    enum EnemyThinking
    {
        Lost,
        Discover,
        LongRange,
        CloseRange,
    }
    EnemyThinking currentthinking = EnemyThinking.Lost;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    
    void Update()
    {
        switch (currentthinking)
        {
            case EnemyThinking.Lost:
                break;
            case EnemyThinking.Discover:
                break;
            case EnemyThinking.LongRange:
                break;
            case EnemyThinking.CloseRange:
                break;

        }
    }
}
