using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dialog.Syntaxes;

namespace Dialog.CustomInstructions
{
    public class Get : DialogConditional
    {
        public override string Name => "get";
        public override Syntax Syntax => Syntax.Begin()
            .Add("global state name").Or()
            .Add("global state name").Add("operator", "==", "=").AddTrailing("control value").Or()
            .Add("global state name").Add("range");


        public override IEnumerator Evaluate(DialogSystem target, Params args, ConditionalResult output)
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

            if (!stateList.StateExists(splitStateName[0]))
            {
                output.Result = false;
                yield break;
            }

            switch (args.Count)
            {
                case 1:
                    output.Result = stateList.GetBool(splitStateName[0]);
                    yield break;
                case 2:
                    output.Result = Range.Parse(args[1]).IsInRange(stateList.GetDouble(splitStateName[0]));
                    yield break;
                case 3:
                    if (args[0] == "=")
                    {
                        output.Result = stateList.GetString(splitStateName[0]).ToLower() == args.JoinEnd(2).ToLower();
                    }
                    else
                    {
                        output.Result = stateList.GetString(splitStateName[0]) == args.JoinEnd(2);
                    }
                    yield break;
            }
        }
    }
}
