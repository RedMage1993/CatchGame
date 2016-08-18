using UnityEngine;
using UnityEditor; // required
using System.Collections;

[CustomEditor(typeof(GameController))] // attribute using GameController script
public class GCEditor : Editor { // inherit from Editor
    SerializedProperty timeLeftProp;
    float prevTimeLeft, timeLeft;
    SerializedProperty spawnsLeftProp;
    int prevSpawnsLeft, spawnsLeft;

    void OnEnable()
    {
        timeLeftProp = serializedObject.FindProperty("timeLeft");
        timeLeft = prevTimeLeft = timeLeftProp.floatValue;

        spawnsLeftProp = serializedObject.FindProperty("spawnsLeft");
        spawnsLeft = prevSpawnsLeft = spawnsLeftProp.intValue;
    }

    public override void OnInspectorGUI()
    {
        // Watch for changes
        // Depending on which one, update the other.
        timeLeft = timeLeftProp.floatValue;
        if (timeLeft != prevTimeLeft)
        {
            // Calculate how many spawns can be done in timeLeft.
        }

        spawnsLeft = spawnsLeftProp.intValue;
        if (spawnsLeft != prevSpawnsLeft)
        {
            // Calculate how much time may be needed to spawn spawnsLeft times.
        }

        base.OnInspectorGUI(); // continue with draw as normal
    }
}
