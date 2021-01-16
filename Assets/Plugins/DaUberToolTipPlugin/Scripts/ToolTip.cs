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

        void AssignToolTip()
        {
            ToolTipManager.main.SetToolTip(this);
        }
        void UnAssignToolTip()
        {
            ToolTipManager.main.RemoveToolTip(this);
        }

        #region Enter/Exit methods
        private void OnMouseEnter()
        {
            AssignToolTip();
        }
        private void OnMouseExit()
        {
            UnAssignToolTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AssignToolTip();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            UnAssignToolTip();
        }
        #endregion
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