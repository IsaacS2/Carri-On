using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureAirborneState : VultureStateClass
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isGrounded)
        {
            ChildSwitchState((int)AnimalStates.Grounded);
        }
    }

    public override void Jumping()
    {
        ChildSwitchState((int)AnimalStates.Gliding);
    }
}
