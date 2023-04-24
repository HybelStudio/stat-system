using HybelStatSystem.Internal;
using UnityEditor;
using UnityEngine;

using static UnityEditor.EditorGUI;

namespace Hybel.StatSystem.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            DrawProperty(position, property, (position, propValue) => PropertyField(position, propValue, label, true));

        public static void DrawProperty(Rect position, SerializedProperty property, System.Action<Rect, SerializedProperty> drawAction)
        {
            var propValue = property.FindPropertyRelative("value");
            var propEnabled = property.FindPropertyRelative("enabled");

            const int OFFSET = 18;

            using (new DisabledGroupScope(!propEnabled.boolValue))
            {
                position.width -= OFFSET;
                drawAction(position, propValue);
            }

            position.x += position.width + OFFSET - indentLevel * 16;
            position.width = position.height = EditorGUI.GetPropertyHeight(propEnabled);
            position.x -= position.width - 4;
            PropertyField(position, propEnabled, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var propValue = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(propValue);
        }
    }
}
