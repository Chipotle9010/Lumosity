using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialWaypoint : MonoBehaviour
{
    public GameObject goNext;
    public TextMeshPro txt;
    public bool isActiveByDefault;
    public List<GameObject> lsDestructibleObjects;
    public ParticleSystem particle;
    
    protected bool isExhausted;
    
    private void Awake()
    {
        if (!particle)
        {
            particle = GetComponentInChildren<ParticleSystem>();
        }

        if (!isActiveByDefault)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate();
        }
    }

    protected void Activate()
    {
        if (isExhausted) return;

        Debug.Log(name);

        isExhausted = true;

        particle.Play();

        if (particle.isPlaying == true)
        {
            txt.text = "You did it!";
        }

        Debug.Log("Should clean up");
        Invoke("CleanUp", 1);
    }

    private void CleanUp()
    {
        Debug.Log("is cleaning up");
        if (goNext)
        {
            goNext.SetActive(true);
        }

        Debug.Log(goNext);
        lsDestructibleObjects.ForEach(obj => Destroy(obj));
        
        if (txt)
        {
            Destroy(txt.gameObject);
        }

        Destroy(gameObject);
    }
}
