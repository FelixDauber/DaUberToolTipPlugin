using UnityEngine;

namespace DaUberToolTipPlugin
{
    public class BasicFollowMouseOnUI : MonoBehaviour
    {
        RectTransform rectTransform;

        public Vector2 offsetToMouse = new Vector2(0.5f, -0.5f);
        public bool allowLeavingUI = false;
        RectTransform canvasRectTransform;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Canvas canvas = GetComponentInParent<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            UpdatePosition(); 
        }
        void FixedUpdate()
        {
            UpdatePosition();
        }
        void UpdatePosition()
        {
            transform.position = Input.mousePosition + (new Vector3(rectTransform.sizeDelta.x * offsetToMouse.x, rectTransform.sizeDelta.y * offsetToMouse.y, 0));
            AlignWindow();
        }
        public void AlignWindow()
        {
            if (transform.position.x + (rectTransform.sizeDelta.x / 2) > canvasRectTransform.sizeDelta.x || transform.position.x - (rectTransform.sizeDelta.x / 2) < 0)
            {
                MirrorX();
            }
            if (transform.position.y + (rectTransform.sizeDelta.y / 2) > canvasRectTransform.sizeDelta.y || transform.position.y - (rectTransform.sizeDelta.y /2) < 0)
            {
                MirrorY();
            }
        }
        public void MirrorX()
        {
            transform.position += new Vector3(-(rectTransform.sizeDelta.x * offsetToMouse.x)*2, 0, 0);
        }
        public void MirrorY()
        {
            transform.position += new Vector3(0, -(rectTransform.sizeDelta.y * offsetToMouse.y)*2, 0);
        }
    }
}