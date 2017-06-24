using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ThrowableSpawnerData))]
public class ThrowableSpawnerDataEditor : Editor
{
    private ReorderableList _list;

    private void OnEnable()
    {
        _list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("spawnData"),
                true, true, true, true);

        _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("prefabRef"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("weight"), GUIContent.none);
        };

        _list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Throwables");
        };

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
