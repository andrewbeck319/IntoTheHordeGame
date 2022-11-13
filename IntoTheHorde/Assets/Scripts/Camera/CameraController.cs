using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum AnimationSelector
    {
        Normal,
        NpcTalk
    }

    public AnimationSelector Animations;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(AnimationSelector animation)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animation == AnimationSelector.Normal)
        {
            animator.SetTrigger("Normal");
        } else if (animation == AnimationSelector.NpcTalk)
        {
            animator.SetTrigger("TalkAnimation");
        }
    }
}
