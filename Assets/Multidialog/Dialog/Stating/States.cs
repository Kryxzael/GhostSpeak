using Dialog.Syntaxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialog
{
    /// <summary>
    /// Represents a list of states used by dialog systems
    /// </summary>
    public class States : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> _statesInternal = new Dictionary<string, string>();

        /// <summary>
        /// A global state list accessable from everywhere by everything. This collection can be manipulated by the 'set' and 'get' instructions
        /// </summary>
        public static States Global { get; } = new States();

        /// <summary>
        /// Holds the states that are local to characters
        /// </summary>
        internal static Dictionary<CharacterInfo, States> CharacterStates = new Dictionary<CharacterInfo, States>();

        /// <summary>
        /// Gets the stateset of a character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static States StatesOf(CharacterInfo character)
            => CharacterStates.ContainsKey(character) //Does this character have a stateset?
                ? CharacterStates[character] //yes, return it
                : (CharacterStates[character] = new States()); //no, create it and return it (assignments return the value that is being assigned)

        /// <summary>
        /// Checks whether a state is defined in this state list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool StateExists(string name)
            => _statesInternal.ContainsKey(name.ToLower());



        /*
         * Accesors
         */

        /// <summary>
        /// Gets a state as a string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            if (!StateExists(name))
            {
                throw new NullReferenceException("The state '" + name + "' does not exist in the current context");
            }

            return _statesInternal[name.ToLower()];
        }

        /// <summary>
        /// Gets a state as a boolean
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetBool(string name)
            => ValueParser.ToBool(GetString(name)) 
                ?? throw new InvalidCastException("The state '" + name + "' cannot be represented as a boolean");

        /// <summary>
        /// Gets a state as an integer
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetInt(string name)
            => ValueParser.ToInt(GetString(name))
                ?? throw new InvalidCastException("The state '" + name + "' cannot be represented as an integer");

        /// <summary>
        /// Gets a state as a float
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetFloat(string name)
            => ValueParser.ToFloat(GetString(name))
                ?? throw new InvalidCastException("The state '" + name + "' cannot be represented as a float");

        /// <summary>
        /// Gets a state as a double
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public double GetDouble(string name)
            => ValueParser.ToDouble(GetString(name))
                ?? throw new InvalidCastException("The state '" + name + "' cannot be represented as a double");


        /*
         * Mutators
         */

        /// <summary>
        /// Sets the value of a state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, string value)
            => _statesInternal[name.ToLower()] = value;

        /// <summary>
        /// Sets the value of a state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, bool value)
            => Set(name, ValueParser.ToString(value));

        /// <summary>
        /// Sets the value of a state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, int value)
            => Set(name, ValueParser.ToString(value));

        /// <summary>
        /// Sets the value of a state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, float value)
            => Set(name, ValueParser.ToString(value));

        /// <summary>
        /// Sets the value of a state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, double value)
            => Set(name, ValueParser.ToString(value));

        /*
         * Special mutators
         */

        /// <summary>
        /// Increments a numeric state
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public void Increment(string name, double amount = 1.0D)
            => Set(name, (StateExists(name) ? GetDouble(name) : 0.0D) + 1);

        /// <summary>
        /// Decrements a numeric state
        /// </summary>
        /// <param name="name"></param>
        public void Decrement(string name)
            => Increment(name, -1.0D);

        /// <summary>
        /// Inverses a boolean state
        /// </summary>
        /// <param name="name"></param>
        public void InvertBoolean(string name)
            => Set(name, !(StateExists(name) ? GetBool(name) : false));

        /// <summary>
        /// Gets the IEnumerator instance of this state list
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => _statesInternal.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
