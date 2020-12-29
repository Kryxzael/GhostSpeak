using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialog.OptionsBox
{
    /// <summary>
    /// Handles an option/selection box
    /// </summary>
    public class OptionsBox : MonoBehaviour
    {
        [Header("Components")]
        public List<OptionControl> OptionControls;
        public AudioSource SelectionChangeSoundPlayer;
        public AudioSource ConfirmationSoundPlayer;

        [Header("Settings")]
        public bool HorizontalControlLayout;

        public int ActiveOptionCount => OptionControls.Count(i => i.IsInUse);

        public int SelectedIndex { get; private set; }
        public OptionControl SelectedOption => OptionControls[SelectedIndex];

        public bool IsOpen { get; private set; }

        private void Start()
        {
            CloseOptionsBox();
        }

        private void Update()
        {
            if (IsOpen)
            {
                if ((Input.GetButtonDown("Up") && !HorizontalControlLayout) || Input.GetButtonDown("Left"))
                {
                    SelectedOption.OnDeselected();
                    SelectedIndex = NegativeMod(SelectedIndex - 1, ActiveOptionCount);
                    SelectedOption.OnSelected();
                    if (SelectionChangeSoundPlayer != null) SelectionChangeSoundPlayer.Play();
                }
                else if ((Input.GetButtonDown("Down") && !HorizontalControlLayout) || Input.GetButtonDown("Right"))
                {
                    SelectedOption.OnDeselected();
                    SelectedIndex = NegativeMod(SelectedIndex + 1, ActiveOptionCount);
                    SelectedOption.OnSelected();
                    SelectionChangeSoundPlayer?.Play();
                }
                else if (Input.GetButton("Submit"))
                {
                    CloseOptionsBox();
                    if (ConfirmationSoundPlayer != null) ConfirmationSoundPlayer.Play();
                }
            }
        }

        public void OpenOptionsBox()
        {
            if (ActiveOptionCount == 0)
            {
                throw new System.InvalidOperationException("Cannot open empty options box");
            }

            SelectedOption.OnDeselected();
            SelectedIndex = 0;
            SelectedOption.OnSelected();

            IsOpen = true;
            transform.localScale = transform.localScale.SetY(1);
        }

        public void CloseOptionsBox()
        {
            IsOpen = false;
            transform.localScale = transform.localScale.SetY(0);
        }

        public OptionControl GetFirstUnusedOption()
            => OptionControls.OrderBy(i => i.ID).FirstOrDefault(i => !i.IsInUse);

        public void ResetState()
            => OptionControls.ForEach(i => i.ResetState());

        private static int NegativeMod(int x, int n)
            => (x % n + n) % n;
    }
}

