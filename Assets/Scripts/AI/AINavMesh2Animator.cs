using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINavMesh2Animator : AIBase
{    
    Animator cmpAnimator;
    float lerpedVelocity;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        cmpAnimator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        lerpedVelocity = Mathf.Lerp(lerpedVelocity, cmpAgent.velocity.magnitude, Time.deltaTime * 2.5f);
        cmpAnimator.SetFloat("speed", lerpedVelocity);
    }
}
