using UnityEngine;
using UnityEngine.UI;

namespace DaUberToolTipPlugin
{
    public class ToolTipObject : MonoBehaviour
    {
        [SerializeField]
        private Text _descriptionText;

        public virtual string Description { get => _descriptionText.text; set => _descriptionText.text = value; }
        public virtual bool Active { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    }
}