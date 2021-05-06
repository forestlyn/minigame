using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Database : ScriptableObject
{
    public bool idle;
    public bool running;
    public bool jumping;
    public bool falling;

    public bool isLying;
    public bool up;

    public bool accumulate;

}
