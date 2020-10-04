using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarInput : MonoBehaviour
{
    public abstract InputState GetInput();
}

public struct InputState
{
    public float HorizontalSteeringAxis;
    public float VerticalSteeringAxis;
    public bool IsBraking;
    public bool Fire;
    public bool SelectPrevWeapon;
    public bool SelectNextWeapon;
}
