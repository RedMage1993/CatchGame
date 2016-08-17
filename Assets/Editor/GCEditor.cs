using UnityEngine;
using UnityEditor; // required
using System.Collections;

[CustomEditor(typeof(GameController))] // attribute using GameController script
public class GCEditor : Editor { // inherit from Editor

    public override void OnInspectorGUI()
    {


        base.OnInspectorGUI(); // continue with draw as normal
    }
}
