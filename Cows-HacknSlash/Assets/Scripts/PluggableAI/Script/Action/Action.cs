using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare as Abstract class cause we don't need to instantiate it.
// This class will act as Base class for all other action we are going to create.
public abstract class Action : ScriptableObject {
    public abstract void Act(StateController controller);
}
