using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

namespace DaUberToolTipPlugin
{
    public class ToolTip : MonoBehaviour, IToolTip, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        protected string description;

        public virtual string Description { get => description; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipManager.main.SetToolTip(this);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipManager.main.RemoveToolTip(this);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ToolTip))]
    public class ToolTipEditor : Editor
    {
        SerializedProperty description;
        public void OnEnable()
        {
            description = serializedObject.FindProperty("description");
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(description);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}