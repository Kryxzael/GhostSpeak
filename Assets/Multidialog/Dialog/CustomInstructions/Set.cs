using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;
using UnityEngine;

namespace Dialog.CustomInstructions
{
    /// <summary>
    /// Instructs a dialog system to set a state (global or for a character)
    /// </summary>
    public class Set : CustomInstruction
    {
        public override string Name => "set";

        public override Syntax Syntax => Syntax.Begin()
            .Add("global state name").Add("operator", "=").AddTrailing("value").Or()
            .Add("global state name").Add("operator", "+=", "-=", "*=", "/=").Add("value", Range.INFINITY, false).Or()
            .Add("global state name").Add("operator", "!");

        public override IEnumerator Execute(DialogSystem target, Params args)
        {
            States stateList;

            string[] splitStateName = args[0].Split('@');
            if (splitStateName.Length > 1)
            {
                stateList = States.StatesOf(target.GetCharacterById(string.Join("@", splitStateName.Skip(1))));
            }
            else
            {
                stateList = States.Global;
            }

            switch (args[1])
            {
                case "=":
                    stateList.Set(
                        name: splitStateName[0], 
                        value: args.JoinEnd(2)
                    );
                    yield break;
                case "+=":
                    stateList.Set(
                        name: splitStateName[0], 
                        value: stateList.GetDouble(splitStateName[0]) + args.ToInt(2)
                    );
                    yield break;
                case "-=":
                    stateList.Set(
                        name: splitStateName[0], 
                        value: stateList.GetDouble(splitStateName[0]) - args.ToInt(2)
                    );
                    yield break;
                case "*=":
                    stateList.Set(
                        name: splitStateName[0], 
                        value: stateList.GetDouble(splitStateName[0]) * args.ToInt(2)
                    );
                    yield break;
                case "/=":
                    stateList.Set(
                        name: splitStateName[0], 
                        value: stateList.GetDouble(splitStateName[0]) / args.ToInt(2)
                    );
                    yield break;
                case "!":
                    stateList.Set(
                        name: splitStateName[0],
                        value: !stateList.GetBool(splitStateName[0])
                    );
                    yield break;
            }


        }
    }
}
