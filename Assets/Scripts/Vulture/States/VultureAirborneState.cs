using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureAirborneState : VultureStateClass
{
    protected override void Update()
    {
        base.Update();

        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Jumping()
    {
        ChildSwitchState((int)AnimalStates.Gliding);
    }
}
