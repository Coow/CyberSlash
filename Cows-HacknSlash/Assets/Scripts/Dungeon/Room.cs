using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room {
    public bool OpenBottom;
    public bool OpenLeft;
    public bool OpenRight;
    public bool OpenTop;

    public List<GameObject> Rooms;
}
