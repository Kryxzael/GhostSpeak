using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.OptionsBox
{
    /// <summary>
    /// Represents a label that will hold the state of a dialogue option. This control is initialized by running a option box
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class OptionControl : MonoBehaviour
    {
        /// <summary>
        /// The dialog system that this dialog box will target
        /// </summary>
        public OptionsBox ParentBox;

        [Tooltip("Numeric id of this option control. The first control should have ID 0, then 1, then 2, etc.")]
        public int ID;

        /// <summary>
        /// The renderer that will draw the text
        /// </summary>
        [HideInInspector]
        public TextMeshProUGUI TextRenderer;

        /// <summary>
        /// The dialouge event to run if this option is selected.
        /// </summary>
        [HideInInspector]
        public string TargetDialogIdentifier;

        public bool IsInUse { get; private set; }

        private void Awake()
        {
            TextRenderer = GetComponent<TextMeshProUGUI>();
            TextRenderer.color = Color.black;
        }

        public void Initialize(string text, string targetIdentifier)
        {
            TextRenderer.text = text;
            TargetDialogIdentifier = targetIdentifier;
            IsInUse = true;
        }

        /// <summary>
        /// Resets the option control
        /// </summary>
        public void ResetState()
        {
            TextRenderer.text = "";
            TargetDialogIdentifier = null;
            IsInUse = false;
        }

        /// <summary>
        /// When the option is selected, it will turn yellow
        /// </summary>
        public void OnSelected()
        {
            TextRenderer.color = Color.blue;
        }

        /// <summary>
        /// When the option is deselected, it will turn white
        /// </summary>
        public void OnDeselected()
        {
            TextRenderer.color = Color.black;
        }

        /// <summary>
        /// When the option is clicked, it will run its target event
        /// </summary>
        public void OnClicked()
        {
            //TODO: Just implement this plz
        }
    }
}
