using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ObjectTrackerTool : EditorWindow
{
    [MenuItem("ADG/Object tracker")]
    static void Init() {
        ObjectTrackerTool window = (ObjectTrackerTool)EditorWindow.GetWindow(typeof(ObjectTrackerTool));
        window.Show();
    }
    
    private bool showTrackedObjects;

    private List<GameObject> trackedObjects;
    private GameObject objectToAdd;


    private void OnGUI() {
        if (Selection.activeGameObject != null) {
            objectToAdd = Selection.activeGameObject;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button($"Add {objectToAdd.name} to the list")) {
                trackedObjects.Add((GameObject)objectToAdd);
            }
        }

        showTrackedObjects = EditorGUILayout.Foldout(showTrackedObjects, "Tracked objects");

        if (showTrackedObjects) {
            foreach (var obj in trackedObjects) {
                GUILayout.BeginHorizontal();
                
                GUI.backgroundColor = Color.white;
                GUILayout.Label(PrefabUtility.GetIconForGameObject(obj), GUILayout.Height(18), GUILayout.Width(18)); 
                obj.SetActive(EditorGUILayout.Toggle(!obj.activeSelf));
                GUILayout.Label(obj.name);
                GUI.backgroundColor = Color.white; 
                if (GUILayout.Button("Show in scene",GUILayout.Width(100))) {
                    Selection.activeTransform = obj.transform;
                    SceneView.lastActiveSceneView.FrameSelected();
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove",GUILayout.Width(100))) {
                    trackedObjects.Remove(obj);
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
