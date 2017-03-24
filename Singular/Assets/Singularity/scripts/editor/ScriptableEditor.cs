using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Singular { 

[CustomEditor(typeof(Scriptable))]
public class ScriptableEditor : Editor
  {
    SerializedObject m_object;
    SerializedProperty dataPath;
    SerializedProperty declarations;
    SerializedProperty script;
    SerializedProperty result;


    void OnEnable()
    {
      m_object = new SerializedObject(target);
      dataPath = m_object.FindProperty("File");
      declarations = m_object.FindProperty("Declarations");
      script = m_object.FindProperty("Script");
      result = m_object.FindProperty("Result");
    }

    public override void OnInspectorGUI()
    {
      m_object.Update();
      
      GUILayout.BeginHorizontal();
      if ( GUILayout.Button("Clear") )
      {
        Scripter.Reset();
      }
      GUILayout.Button("Open");
      GUILayout.Button("Save");
      if (GUILayout.Button("Run")) {
        try {
          result.stringValue = "ok";          
        Scripter.GetEngine().Execute(script.stringValue);
        } catch ( Exception e)
        {
          result.stringValue = e.Message;
          
        }

      };
      GUILayout.EndHorizontal();
      EditorGUILayout.PropertyField(dataPath);
      EditorGUILayout.PropertyField(declarations);
      EditorGUILayout.PropertyField(script);
      EditorGUILayout.PropertyField(result);
      m_object.ApplyModifiedProperties();
      //  File = EditorGUILayout.TextField(File);
    }
  }

  

  }