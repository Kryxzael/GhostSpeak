using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialog.Syntaxes
{
    /// <summary>
    /// Represents a safe list of parameters
    /// </summary>
    public class Params : IEnumerable<string>
    {
        List<string> Internal { get; }

        /// <summary>
        /// Gets the parameter at a specific index
        /// </summary>
        /// <param name="index">Index to get parameter of</param>
        /// <returns></returns>
        public string this[int index]
        {
            get
            {
                if (index >= Internal.Count)
                {
                    return null;
                }
                return Internal[index];
            }
            internal set
            {
                if (index >= Internal.Count)
                {
                    return;
                }
                Internal[index] = value;
            }
        }

        /// <summary>
        /// Gets the amount of paramets this parameter collection has
        /// </summary>
        public int Count => Internal.Count;

        /// <summary>
        /// Creates a prameter collection
        /// </summary>
        /// <param name="values">Values to create Params object from</param>
        public Params(IEnumerable<string> values)
        {
            Internal = values.ToList();
        }

        /// <summary>
        /// Joins all trailing parameters starting at an index
        /// </summary>
        /// <param name="startIndex">Index to start joining from</param>
        /// <returns></returns>
        public string JoinEnd(int startIndex)
        {
            return string.Join(" ", Internal.Skip(startIndex).ToArray());
        }

        /// <summary>
        /// Converts a parameter to an integer
        /// </summary>
        /// <param name="index">Index of parameter to convert</param>
        /// <returns></returns>
        public int ToInt(int index)
        {
            double? v = ValueParser.ToDouble(this[index]);

            if (!v.HasValue)
            {
                throw new InvalidCastException("NaN");
            }

            if (Math.Round(v.Value) != v.Value)
            {
                throw new InvalidCastException("Not an integer");
            }

            try
            {
                return (int)Math.Round(v.Value);
            }
            catch (Exception)
            {
                throw new InvalidCastException("Conversion not possible");
            }

        }

        /// <summary>
        /// Converts a parameter to a float
        /// </summary>
        /// <param name="index">Index of parameter to convert</param>
        /// <returns></returns>
        public float ToFloat(int index)
        {
            float? v = ValueParser.ToFloat(this[index]);

            if (!v.HasValue)
            {
                throw new InvalidCastException("NaN");
            }

            return v.Value;
        }

        /// <summary>
        /// Converts a parameter to a double
        /// </summary>
        /// <param name="index">Index of parameter to convert</param>
        /// <returns></returns>
        public double ToDouble(int index)
        {
            double? v = ValueParser.ToDouble(this[index]);

            if (!v.HasValue)
            {
                throw new InvalidCastException("NaN");
            }

            return v.Value;
        }

        /// <summary>
        /// Converts a parameter to a boolean
        /// </summary>
        /// <param name="index">Index of parameter to convert</param>
        /// <returns></returns>
        public bool ToBoolean(int index)
        {
            bool? v = ValueParser.ToBool(this[index]);

            if (!v.HasValue)
            {
                throw new InvalidCastException("NaN");
            }

            return v.Value;
        }

        /// <summary>
        /// Checks whether an argument can be represented as a float
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsFloat(int index)
        {
            return ValueParser.ToFloat(this[index]).HasValue;
        }

        /// <summary>
        /// Checks whether an argument can be represented as a double 
        /// </summary>
        /// <param name="index">Index of parameter to check</param>
        /// <returns></returns>
        public bool IsDouble(int index)
        {
            return ValueParser.ToDouble(this[index]).HasValue;
        }

        /// <summary>
        /// Checks whether an argument can be represented as an integer 
        /// </summary>
        /// <param name="index">Index of parameter to check</param>
        /// <returns></returns>
        public bool IsInteger(int index)
        {
            return ValueParser.ToInt(this[index]).HasValue;
        }


        /// <summary>
        /// Checks whether an argument can be represented as a boolean 
        /// </summary>
        /// <param name="index">Index of parameter to check</param>
        /// <returns></returns>
        public bool IsBoolean(int index)
        {
            return ValueParser.ToBool(this[index]).HasValue;
        }

        /// <summary>
        /// Gets the public enumerator for this parameter collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        /// <summary>
        /// Gets the public enumerator for this parameter collection
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        /// <summary>
        /// Returns the string representation of the parameters of this Params object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(" ", this.ToArray());
        }
    }
}
