using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAnimationPlay : MonoBehaviour
{
    Animator animator;
    public string[] animationStateNames;
	// Use this for initialization
	void Start ()
    {
        animator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPlayAnimation(int i)
    {
        animator.Play(animationStateNames[i], 0);
    }
}
