using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    
    private DropScript m_drop;
    private Animator animator;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
          m_drop = GetComponent<DropScript>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
            Break();
    }


    public void Break()
    {
        Debug.Log("clicou");
        animator.SetTrigger("break");
        m_drop.StartCoroutine("iDrop");
    }

}
