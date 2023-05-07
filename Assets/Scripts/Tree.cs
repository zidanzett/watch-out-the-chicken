using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour
{
    static HashSet<Vector3> positionSet = new HashSet<Vector3>();

    public static HashSet<Vector3> AllPositions {
        get => new HashSet<Vector3>(positionSet);
    }

    private void OnEnable() {
        positionSet.Add(this.transform.position);
    }

    private void OnDisable() {
        positionSet.Remove(this.transform.position);
    }
}
