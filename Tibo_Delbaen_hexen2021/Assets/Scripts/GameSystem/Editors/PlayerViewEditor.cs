using GameSystem.Utils;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace GameSystem.Editors
{
    [CustomEditor(typeof(PlayerView))]
    public class PlayerViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            var positionHelperSp = serializedObject.FindProperty("_positionHelper");
            EditorGUILayout.PropertyField(positionHelperSp);

            var movementNameSp = serializedObject.FindProperty("_movementName");
            var movementName = movementNameSp.stringValue;

            var typeNames = MoveCommandProviderTypeHelper.FindMoveCommandProviderTypes();

            var selectedIdx = Array.IndexOf(typeNames, movementName);
            var newSelectedIdx = EditorGUILayout.Popup("Movement", selectedIdx, typeNames);

            if (selectedIdx != newSelectedIdx)
                movementNameSp.stringValue = typeNames[newSelectedIdx];

            serializedObject.ApplyModifiedProperties();
        }
    }
}
