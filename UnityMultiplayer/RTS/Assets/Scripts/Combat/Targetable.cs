using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : NetworkBehaviour
{
    [SerializeField] Transform aimAtPoint = null;

    public Transform GetAimPopint()
    {
        return aimAtPoint;
    }
}
