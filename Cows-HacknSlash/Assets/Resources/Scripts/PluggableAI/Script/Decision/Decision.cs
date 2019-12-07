using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Class for Decisions
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}
