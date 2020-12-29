using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    /// <summary>
    /// Allows a Text UI element to make crawling (scrolling) text
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(AudioSource))]
    public class CrawlingText : MonoBehaviour
    {
        /// <summary>
        /// The text to show in box
        /// </summary>
        public string Text;

        /// <summary>
        /// The delay between every character in the text scroll
        /// </summary>
        public float TextDelay;

        /// <summary>
        /// Is the text currently slowed down?
        /// </summary>
        public bool Slowdown;

        /// <summary>
        /// The text label target
        /// </summary>
        private TextMeshProUGUI _textField;

        /// <summary>
        /// The audio source for text sounds
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        /// DEBUG: Skips the text crawling
        /// </summary>
        public static bool DebugSkipText;

        /// <summary>
        /// The current text effects
        /// </summary>
        public TextEffectMode Effects;

        private bool _skippingScroll;
        private Vector3 _initialPosition;

        private LFO _rotationLFO;
        private LFO _twitchingLFO;

        /// <summary>
        /// Is this TextCrawler currently scrolling text
        /// </summary>
        public bool Scrolling { get; private set; }

        /// <summary>
        /// The audiosource that will be played randomly when a character is pressed
        /// </summary>
        public AudioClip Audio
        {
            get => _audioSource.clip;
            set => _audioSource.clip = value;
        }

        /*
         * Listen here you little shit.
         * You may see that there is both a Start() and Awake() event handler here
         * and think to yourself "Hm. I should probably combine these".
         * Well about that:
         * FUCK NO, AND FUCK YOU!
         * It breaks shit.
         * Just don't
         * ¯\_(ツ)_/¯
         */
        private void Start()
        {
            _initialPosition = transform.position;
            _textField.richText = true;
        }

        private void Awake()
        {
            _textField = GetComponent<TextMeshProUGUI>();
            _audioSource = GetComponent<AudioSource>();
            _rotationLFO = LFO.CreateSine();
            _twitchingLFO = LFO.CreatePulse(0.1f);
        }

        /// <summary>
        /// Applies text effects
        /// </summary>
        private void Update()
        {
            transform.position = _initialPosition;
            transform.SetEuler2D(0);

            //Applies shake effect
            if (Effects.HasBitFlag(TextEffectMode.Shake))
            {
                const float SHAKE_AMOUNT = 2.5f;

                transform.position = _initialPosition + new Vector3(Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT), Random.Range(-SHAKE_AMOUNT, SHAKE_AMOUNT));

            }

            //Applies rotation effect
            if (Effects.HasBitFlag(TextEffectMode.Rotate))
            {
                const float MAX_ROTATION = 1f;
                const float ROTATION_SPEED = 0.025f;

                transform.SetEuler2D((float)(_rotationLFO.ValueFor(Time.frameCount * ROTATION_SPEED) * 2 * MAX_ROTATION) - MAX_ROTATION);
            }

            //Applies twitching effect
            if (Effects.HasBitFlag(TextEffectMode.Twitch))
            {
                const float TWITCH_AMOUNT = 2.5f;
                const float TWITCH_LFO_SPEED = 0.025f;

                if (_twitchingLFO.ValueFor(Time.frameCount * TWITCH_LFO_SPEED) == 1)
                {
                    transform.position = _initialPosition + new Vector3(Random.Range(-TWITCH_AMOUNT, TWITCH_AMOUNT), Random.Range(-TWITCH_AMOUNT, TWITCH_AMOUNT));
                }
            }

            //Skips text 
            if (Input.GetButtonDown("Submit") && Scrolling)
            {
                _skippingScroll = true;
            }
        }

        /// <summary>
        /// Starts the text scrolling
        /// </summary>
        public void BeginScroll()
        {
            //Stops any running scrolling routines and starts a new one
            StopAllCoroutines();
            StartCoroutine("CoScroll");
        }

        /// <summary>
        /// Jumps the text scroll
        /// </summary>
        public void JumpScroll()
        {
            //Stops any currently running scrolling routines, 
            //updates scrolling state and applies all text to the label
            StopAllCoroutines();
            Scrolling = false;
            _textField.text = Text;
        }

        /// <summary>
        /// Lets the text scroll
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoScroll()
        {
            //Flags the text as currently scrolling. Other coroutines can await for this to finish
            Scrolling = true;

            //Sanitizes the text if needed
            string sanitizedString = SanitizeString(Text);

            //Clears the text of the label
            _textField.text = "";

            //Goes over every character in the text and adds them one by one to the label
            for (int i = 0; i < sanitizedString.Length; i++)
            {
                char c = sanitizedString[i];

                //Gets the time multiplies for the current character
                float waitMultiplier = GetWaitMultiplierAndSkipData(c,
                    out bool skip,  //Out param. Should this character be skiped
                    out bool skipSound); //Out param. Should this character not play a sound

                waitMultiplier *= Slowdown ? 4f : 1f;

                //Character is start of a character/style code
                if (c == '#' && i != sanitizedString.Length - 1)
                {
                    //Add code
                    _textField.text += ReplaceStyleCode(sanitizedString[i + 1]);
                    //Skip next character
                    i++;
                    continue;
                }

                //Appends the character if it should not be skipped
                if (!skip)
                {
                    _textField.text += c;

                    //Plays sound
                    if (Audio != null && !skipSound) _audioSource.Play();
                }

                //Delays the process by a given time to make a scrolling effect
                if (!DebugSkipText && !_skippingScroll)
                {
                    yield return new WaitForSeconds(TextDelay * waitMultiplier);
                }

            }

            //Unflags this object as scrolling and scroll skipping
            Scrolling = false;
            _skippingScroll = false;

            //Reset slowdown mode
            Slowdown = false;

        }

        /// <summary>
        /// Returns the value to multiply the TextDelay value with for a given character and whether this character should be skipped by the crawler or not
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <param name="skip">If this out param is true, the character should not be displayed by the text</param>
        /// <returns></returns>
        private float GetWaitMultiplierAndSkipData(char c, out bool skip, out bool skipSound)
        {
            skip = false;
            skipSound = false;

            switch (c)
            {
                case '\n':
                case '\r':
                    skipSound = true;
                    return 0f;
                case ' ':
                    skipSound = true;
                    return 1f;
                case '.':
                case '?':
                case '!':
                case ':':
                    skipSound = true;
                    return 2f;
                case ',':
                case ';':
                    skipSound = true;
                    return 1.75f;
                case '^':
                    skip = true;
                    return 5f;
                case '~':
                    skip = true;
                    return 10f;
            }

            return 1;
        }

        /// <summary>
        /// Returns the respective TMP rich text code for the given character code
        /// </summary>
        /// <param name="styleId"></param>
        /// <returns></returns>
        private string ReplaceStyleCode(char styleId)
        {
            switch (styleId)
            {
                //Red text
                case 'r':
                    return "</color><color=\"red\">";

                //Green text
                case 'g':
                    return "</color><color=\"green\">";

                //Blue text
                case 'b':
                    return "</color><color=\"blue\">";

                //Yellow text
                case 'y':
                    return "</color><color=\"yellow\">";

                //Purple text
                case 'p':
                    return "</color><color=\"purple\">";

                //Orange text
                case 'o':
                    return "</color><color=\"orange\">";

                //Reset color (white)
                case 'w':
                    return "</color>";

                //Bold
                case 'B':
                    return "<b>";

                //Italic
                case 'I':
                    return "<i>";

                //Reset style (bold, italic, alpha, size)
                case 'R':
                    return "</b></i><alpha=#FF></size>";

                //Small text
                case 'S':
                    return "<size=50%>";

                //Huge text
                case 'H':
                    return "<size=150%>";

                //Semi-transparent text
                case 'T':
                    return "<alpha=#88>";

                //Reset all styling
                case '0':
                    return "</color></b></i><alpha=#FF><size=100%>";

                //Slur characters
                case '!':
                    Debug.Log(_textField.color.ToString());
                    return "<mark=#000000FF>A</mark>";

                //Escape #
                case '#':
                    return "#";
            }

            Debug.LogWarning("Invalid style code in dialog: '#" + styleId + "'");
            return "";
        }

        /// <summary>
        /// Does any sanitization on a string (currently none)
        /// </summary>
        /// <param name="str">String to sanitize</param>
        /// <returns></returns>
        private string SanitizeString(string str)
        {
            return str;
        }

        /// <summary>
        /// Describes the different effects text can have
        /// </summary>
        [System.Flags]
        public enum TextEffectMode
        {
            /// <summary>
            /// The text will not have any effects
            /// </summary>
            None = 0x0,

            /// <summary>
            /// The text will be shaking
            /// </summary>
            Shake = 0x1,

            /// <summary>
            /// The text will be waving
            /// </summary>
            Rotate = 0x2,

            /// <summary>
            /// The text will be twitching
            /// </summary>
            Twitch = 0x4
        }

    }
    }
