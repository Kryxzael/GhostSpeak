using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dialog.Internals;
using Dialog.Internals.Instructions;
using Dialog.Syntaxes;

namespace Dialog
{

    /// <summary>
    /// Controls the dialog system
    /// </summary>
    public class DialogSystem : MonoBehaviour
    {
        /*
         * Unity editor stuff
         */
        [Header("Components")]
        public TextMeshProUGUI NameLabel;
        public TextMeshProUGUI TextLabel;
        public Image PortraitContainer;
        public OptionsBox.OptionsBox OptionsBox;

        [Header("Sounds")]
        public AudioSource OpenSoundPlayer;
        public AudioSource CloseSoundPlayer;

        [Header("Dialog bank and characters")]
        [Description("Name of the bank to use")]
        public string Bank;

        [Description("The character that will be used by default if no other character is specified, or as a fallback")]
        public string DefaultCharacter;

        [Description("The characters loaded into this dialog system")]
        public List<CharacterInfo> Characters;

        [Description("The custom instructions loaded into this dialog system")]
        public List<CustomInstruction> CustomInstructions = new List<CustomInstruction>();

        /*
         * Non-editor stuff
         */

        /// <summary>
        /// The character that is currently speaking, or most recently spoke
        /// </summary>
        public CharacterInfo CurrentCharacter { get; private set; }

        /// <summary>
        /// The portrait that was most recently loaded
        /// </summary>
        public Sprite CurrentPortrait { get; private set; }


        /// <summary>
        /// Is a dialog currently taking place?
        /// </summary>
        public bool InDialog { get; private set; }

        /// <summary>
        /// Is the dialog box open?
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// The amount of scopes that are being skipped (Used for branching)
        /// </summary>
        public int SkippingScopes { get; set; }

        private void Start()
        {
            transform.localScale = transform.localScale.SetY(0);

            //Activate custom instructions
            foreach (Type i in System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(i => typeof(CustomInstruction).IsAssignableFrom(i) && i.GetConstructor(new Type[0]) != null))
            {
                try
                {
                    CustomInstructions.Add(Activator.CreateInstance(i) as CustomInstruction);
                }
                catch (Exception)
                {
                    Debug.LogWarning("Unable to instanitate custom instruction type: '" + i + "'");
                }

            }
        }


        /// <summary>
        /// Starts a dialog sequence
        /// </summary>
        /// <param name="dialogName"></param>
        public void StartDialog(string dialogName)
        {
            if (InDialog) StopDialog();
            StartCoroutine(nameof(CoDialogLoop), dialogName);
        }

        /// <summary>
        /// Stops a dialog, regardless of its current state
        /// </summary>
        public void StopDialog()
        {
            InDialog = false;
            StopCoroutine(nameof(CoDialogLoop));
            CloseTextBox();

            Debug.Log("Dialog ended");
        }

        /// <summary>
        /// Main dialog sequence
        /// </summary>
        /// <param name="dialogName"></param>
        /// <returns></returns>
        private IEnumerator CoDialogLoop(string dialogName)
        {
            //Start dialog
            InDialog = true;
            Debug.Log("Dialog '" + dialogName + "' started");
            ChangeActiveCharacter(DefaultCharacter);

            //Create parser
            DialogBankParser parser = new DialogBankParser(Bank, dialogName, this);
            DialogInstruction currentInstruction;

            //Fetch, decode, execute loop
            //This goes on until a EndOfDialogInstruction has been executed
            do
            {
                currentInstruction = parser.MoveNext();

                //Skip skippable scopes
                if (SkippingScopes > 0 && !(currentInstruction is CloseScopeInstruction || currentInstruction is ConditionalInstructionInstruction))
                {
                    continue;
                }

                yield return currentInstruction.Execute(this);

            } while (!(currentInstruction is EndOfDialogInstruction));

            //End dialog
            StopDialog();
        }

        /// <summary>
        /// Resets the dialog box back to its original state
        /// </summary>
        public void ResetDialogAndOptions()
        {
            TextLabel.GetComponent<CrawlingText>().Text = "";
            TextLabel.GetComponent<CrawlingText>().Effects = CrawlingText.TextEffectMode.None;
            TextLabel.text = "";
            OptionsBox.ResetState();
        }

        /// <summary>
        /// Sets the character that is currently speaking
        /// </summary>
        /// <param name="identifier"></param>
        public void ChangeActiveCharacter(string identifier)
        {
            //Get character by identifier
            CurrentCharacter = GetCharacterById(identifier);

            //Set name label
            NameLabel.text = CurrentCharacter.Name;

            //Set portrait to character's default portrait
            SetActivePortrait(CurrentCharacter.DefaultPortraitIdentifier);

            //Set text-crawler's crawl sound and text delay
            TextLabel.GetComponent<CrawlingText>().Audio = CurrentCharacter.SpeakSound;
            TextLabel.GetComponent<CrawlingText>().TextDelay = CurrentCharacter.DefaultTextDelay;
        }

        /// <summary>
        /// Sets the portrait picture
        /// </summary>
        /// <param name="identifier"></param>
        public void SetActivePortrait(string identifier)
        {
            CurrentPortrait = GetPortraitById(CurrentCharacter, identifier);

            if (PortraitContainer != null)
                PortraitContainer.sprite = CurrentPortrait;
        }

        /// <summary>
        /// Opens the textbox
        /// </summary>
        public void OpenTextBox()
        {
            if (IsOpen) return;

            transform.localScale = transform.localScale.SetY(1);
            IsOpen = true;
            if (OpenSoundPlayer != null) OpenSoundPlayer.Play();
        }

        /// <summary>
        /// Closes the textbox
        /// </summary>
        public void CloseTextBox()
        {
            if (!IsOpen) return;

            transform.localScale = transform.localScale.SetY(0);
            IsOpen = false;
            if (CloseSoundPlayer != null) CloseSoundPlayer.Play();
        }

        /// <summary>
        /// Runs a custom instruction on this dialog system by name
        /// </summary>
        /// <param name="instructionName"></param>
        /// <param name="args"></param>
        public IEnumerator RunCustomInstructionAsync(string instructionName, Params args)
        {
            //Load instruction
            CustomInstruction instr = CustomInstructions
                .SingleOrDefault(i => i.Name.ToLower() == instructionName.ToLower());

            //Instruction wasn't found
            if (instr == null)
            {
                Debug.LogError("Invalid custom instruction. No such instruction with name '" + instructionName + "'");
                yield break;
            }

            //Check syntax
            if (!instr.Syntax.IsCorrectSyntax(args))
            {
                Debug.LogError("The syntax of the custom instruction, '" + instructionName + "', is incorrect:\n" + instr.Syntax.ToString());
                yield break;
            }
                

            //Execute the instruction
            yield return instr.Execute(this, args);
        }

        /// <summary>
        /// Evalutates a dialog conditional by name and store output in the ConditionalResult. Conditionals are stored together with other custom instructions
        /// </summary>
        /// <param name="conditional"></param>
        /// <param name="args"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public IEnumerator EvalutateConditionalAsync(string conditional, Params args, DialogConditional.ConditionalResult output)
        {
            //Load conditional
            DialogConditional instr = CustomInstructions
                .OfType<DialogConditional>()
                .SingleOrDefault(i => i.Name.ToLower() == conditional.ToLower());

            //Conditional not found
            if (instr == null)
            {
                Debug.LogError("Invalid conditional. No such conditional with name '" + conditional + "'");
                yield break;
            }

            //Check syntax
            if (!instr.Syntax.IsCorrectSyntax(args))
            {
                Debug.LogError("The syntax of the conditional, '" + conditional + "', is incorrect:\n" + instr.Syntax.ToString());
                yield break;
            }
                

            //Evalutate conditional
            yield return instr.Evaluate(this, args, output);

            //Store output for future use by previous-conditional
            Conditionals.Previous.PreviousResult = output;
        }


        /*
         * Helpers
         */

        /// <summary>
        /// Gets a registered CharacterInfo instance whose identifier matches the argument, or the default character as a fallback
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CharacterInfo GetCharacterById(string id)
        {
            //Attempt to find character by id
            CharacterInfo character = Characters.SingleOrDefault(i => i.Identifier == id);

            //Character wasn't found, fallback to default character and warn the programmer
            if (character == null)
            {
                Debug.LogError("No such character with identifier '" + id + "'");
                return Characters.SingleOrDefault(i => i.Identifier == DefaultCharacter)
                    ?? throw new NullReferenceException("DialogSystem does not have a fallback character");
            }

            return character;
        }

        /// <summary>
        /// Gets the portrait sprite of the given character whose identifier matches the argument, or the default portrait as a fallback
        /// </summary>
        /// <param name="character"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sprite GetPortraitById(CharacterInfo character, string id)
        {
            //Attempt to find portrait by character and id
            Sprite spr = character.Portraits.SingleOrDefault(i => i.Identifier == id)?.Image;

            //Portrait wasn't found, fallback to default portrait and warn the programmer
            if (spr == null)
            {
                Debug.LogError("Character with id '" + character.Identifier + "' does not have a portrait with id '" + id + "'");

                return character.Portraits.SingleOrDefault(i => i.Identifier == character.DefaultPortraitIdentifier)?.Image
                    ?? throw new NullReferenceException("Character with id '" + character.Identifier + "' does not have a fallback portrait");
            }

            return spr;
        }
    }
}