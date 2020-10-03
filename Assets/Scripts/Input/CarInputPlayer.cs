using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputPlayer : CarInput
{
    public override InputState GetInput()
    {
        // return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return new InputState
        {
            HorizontalSteeringAxis = Input.GetAxis("Horizontal"),
            VerticalSteeringAxis = Input.GetAxis("Vertical"),
            IsBraking = Input.GetKey(KeyCode.Space),
        };
    }
}
