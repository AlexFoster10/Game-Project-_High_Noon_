using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    NavMeshAgent agent;
    Animator animator;
    public float barkTimer;
    [SerializeField] AudioClip[] barkSFX;

    void Start()
    {
        barkTimer = Random.Range(0f, 15.0f);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        barkTimer -= Time.deltaTime;   
        agent.destination = playerTransform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (barkTimer <= 0f)
        {
            bark();
        }
    }



    void bark()
    {
        sfxManager.instance.playRandSFX(barkSFX, transform.transform, 0.7f);
        barkTimer = Random.Range(0f, 15.0f);
    }
}
