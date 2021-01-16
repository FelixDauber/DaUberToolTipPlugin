using UnityEngine;
using UnityEditor;

namespace DaUberToolTipPlugin
{
    public class ToolTipManager : MonoBehaviour
    {
        public IToolTip currentToolTip;
        public static ToolTipManager main;

        //Determines what's used for the tooltip. Prefabs are spawned on awake.
        public ToolTipObject toolTipObject;
        [SerializeField]
        protected bool toolTipIsAPrefab;


        public void SetToolTip(IToolTip toolTip)
        {
            if (toolTip == null)
            {
                SetToolTipActive(false);
            }
            else
            {
                currentToolTip = toolTip;
                SetToolTipActive(true);
            }
        }
        public void RemoveToolTip(IToolTip toolTip)
        {
            if(currentToolTip == toolTip)
            {
                currentToolTip = null;
                SetToolTipActive(false);
            }
        }
        public void SetToolTipActive(bool isActive)
        {
            if (isActive && currentToolTip.Description != "")
            {
                toolTipObject.Active = true;
                toolTipObject.Description = currentToolTip.Description;
            }
            else
            {
                toolTipObject.Active = false;
            }
        }

        void Awake()
        {
            //Check existing main
            CheckIfDuplicate();

            //Find and set up toolTipObject
            CheckToolTipObject();
        }

        void CheckIfDuplicate()
        {
            if (main == null || main == this)
            {
                main = this;
            }
            else
            {
                Debug.LogWarning("Detected more than one toolTipManager, Destroying one.", this);
                Destroy(this);
            }
        }

        void CheckToolTipObject()
        {
            if (toolTipObject == null)
            {
                Debug.LogWarning("AutoAssigning ToolTipObject for ToolTipManager", this);
                toolTipObject = FindObjectOfType<ToolTipObject>(true);
                if (toolTipObject == null)
                {
                    Debug.LogError("No toolTipObject referenced or found in the entire scene! Deleting ToolTipManager!", this);
                    Destroy(this);
                }
            }

            if (toolTipIsAPrefab)
                toolTipObject = Instantiate(toolTipObject, transform);
            else
                toolTipObject.transform.SetParent(transform);

            toolTipObject.Active = false;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            ToolTipObjectIsAPrefab();
        }

        bool ToolTipObjectIsAPrefab()
        {
            if (toolTipObject == null || toolTipObject.gameObject.scene.name != null)
            {
                toolTipIsAPrefab = false;
                return false;
            }
            toolTipIsAPrefab = true;
            return true;
        }
#endif

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(ToolTipManager))]
    public class ToolTipManagerEditor : Editor
    {
        SerializedProperty toolTipObject;
        SerializedProperty toolTipObjectIsAPrefab;
        public void OnEnable()
        {
            toolTipObject = serializedObject.FindProperty("toolTipObject");
            toolTipObjectIsAPrefab = serializedObject.FindProperty("toolTipIsAPrefab");
        }
        public override void OnInspectorGUI()
        {

            EditorGUILayout.PropertyField(toolTipObject);
            if((target as ToolTipManager).toolTipObject != null)
                EditorGUILayout.PropertyField(toolTipObjectIsAPrefab);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}