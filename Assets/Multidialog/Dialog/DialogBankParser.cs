using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Dialog.Internals.Instructions;

namespace Dialog.Internals
{
    /// <summary>
    /// Parses dialog files
    /// </summary>
    public class DialogBankParser
    {
        /// <summary>
        /// Name of the bank to parse
        /// </summary>
        public string BankName { get; }

        /// <summary>
        /// Identifier of the dialog to parse
        /// </summary>
        public string DialogIdentifier { get; }

        /// <summary>
        /// Dialog system to target
        /// </summary>
        public DialogSystem Target { get; }

        /// <summary>
        /// Has this DialogBankParser been initialized?
        /// </summary>
        public bool Initialized { get; private set; }

        /*
         * Data
         */

        /// <summary>
        /// The contents of the dialog
        /// </summary>
        private string[] _labelContents = null;

        /// <summary>
        /// The line that will NEXT BE READ (Post execution increment)
        /// </summary>
        private int _lineNr;


        public DialogBankParser(string bankName, string entryLabel, DialogSystem target)
        {
            BankName = bankName;
            DialogIdentifier = entryLabel;
            Target = target;
        }

        /// <summary>
        /// Reads the bank and label
        /// </summary>
        public void Init()
        {
            //Load file
            TextAsset asset = Resources.Load<TextAsset>(BankName);
            if (asset == null) throw new ArgumentException("Could not load dialog bank '" + BankName + "'");

            List<string> output = new List<string>();
            bool reading = false; //This flag will be set when the intended label is found

            //Locate label
            foreach (string line in asset.text.Replace("\r", "").Split(new[] { '\n' }))
            {
                //Entry label was found, start reading lines
                if (line.Trim() == "::" + DialogIdentifier)
                {
                    reading = true;
                    continue;
                }

                //While reading lines, add them to the output buffer
                if (reading)
                {
                    //We have found the start of a new label, stop reading
                    if (line.Trim().StartsWith("::"))
                    {
                        break;
                    }

                    output.Add(line);
                }
            }

            //Copy lines from buffer to contents and initialize state
            _labelContents = output.ToArray();
            _lineNr = 0;
            Initialized = true;
        }

        /// <summary>
        /// Executes the next line of the dialog
        /// </summary>
        public DialogInstruction MoveNext()
        {
            //Initialize parser, if uninitialized
            if (!Initialized)
            {
                Init();
            }

            //We have reached the end of the dialog sequence
            if (_lineNr >= _labelContents.Length)
            {
                return new EndOfDialogInstruction();
            }

            //Read line
            DialogInstruction instruction = ParseLine(_labelContents[_lineNr]);

            //Increment program counter
            _lineNr++;
            return instruction;
        }

        /// <summary>
        /// Parses a string as a line from a dialog bank and returns an instruction for it
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static DialogInstruction ParseLine(string line)
        {
            //Trim comments
            line = Regex.Split(line, "//").First().Trim();



            //Character change sequence
            if (line.StartsWith("@"))
            {
                return new ChangeCharacterInstruction(line.Substring(1));
            }

            //Portrait change sequence
            else if (line.StartsWith("[") && line.EndsWith("]"))
            {
                return new SetPortraitInstruction(line.Substring(1, line.Length - 2));
            }

            //Line is a goto expressions
            else if (line.StartsWith("->"))
            {
                return new GotoInstruction(line.Substring(2));
            }

            //Textbox launcher
            else if (line.StartsWith("-"))
            {
                return new ShowTextBoxInstruction();
            }

            //Effect modifier
            else if (line.StartsWith(">>") && line.EndsWith("<<"))
            {
                return new TextEffectInstruction(line.Substring(2, line.Length - 4));
            }

            //Custom instruction
            else if (line.StartsWith("/"))
            {
                return new CustomInstructionInstruction(line.Substring(1));
            }

            //Conditional
            else if (Regex.IsMatch(line, @"^\?.+\?.+$"))
            {
                string[] split = line.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                return new ConditionalInstructionInstruction(split[0], split[1], false);
            }

            //Inversed conditional
            else if (Regex.IsMatch(line, @"^!.+\?.+$"))
            {
                string[] split = line.Substring(1).Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                return new ConditionalInstructionInstruction(split[0], split[1], true);
            }

            //Line is blank
            else if (string.IsNullOrWhiteSpace(line))
            {
                return new NopInstruction();
            }

            //Dialog option
            else if (line.StartsWith(">"))
            {
                line = line.Substring(1);

                //Dialog option has a target label
                if (Regex.IsMatch(line, ".*->.*"))
                {
                    //Split value based on regex
                    string[] regexSplit = Regex.Split(line.Substring(0), "->")
                        .Select(i => i.Trim())
                        .ToArray();

                    //Assert that no part of expression is empty
                    if (string.IsNullOrEmpty(regexSplit[0]) || string.IsNullOrEmpty(regexSplit[1]))
                    {
                        Debug.LogError("Invalid option specifier '" + line + "'");
                        return new SetDialogOptionInstruction("ERR_BAD_FORMAT");
                    }

                    //Return new dialog option
                    return new SetDialogOptionInstruction(regexSplit[0], regexSplit[1]);
                }
                //Dialog option does not have a target label
                return new SetDialogOptionInstruction(line.Substring(0).Trim());
            }

            //Close scope
            else if (line == "}")
            {
                return new CloseScopeInstruction();
            }

            //Normal text sequence
            else
            {
                return new TextInstruction(line);
            }
        }
    }
}