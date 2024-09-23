using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : TutorialWaypoint
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying || isExhausted) return;

        GameObject go = new GameObject();
        EnemyTutorial e = go.AddComponent<EnemyTutorial>();

        e.goNext = goNext;
        e.txt = txt;
        e.isActiveByDefault = isActiveByDefault;
        e.lsDestructibleObjects = new List<GameObject>(lsDestructibleObjects);
        Debug.Log(e.particle);
        e.particle = particle;
        Debug.Log(e.particle);
        
        e.Activate();

        e.isExhausted = true;
    }
}
