using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPlatform : MonoBehaviour
{
    private Animator animator;
    private bool onTouch = false;
    [SerializeField]
    private float timer,timerOut;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onTouch)
        {
            timer += Time.deltaTime;
        }

        if(timer > timerOut)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            animator.SetBool("isBlinking", true);
            onTouch = true;
        }
        
    }

}
