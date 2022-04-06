using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private bool up;
    public bool getUp()
    {
        return up;
    }

    public void setUp()
    {
        up = !up;
}
}
