using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scriptable_Objects
{
    [CustomEditor(typeof(HexGrid))]
    class HexGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            HexGrid grid = (HexGrid)target;
            if (GUILayout.Button("Reset Grid"))
            {
                grid.ResetGrid();
            }
        }
    }
}
