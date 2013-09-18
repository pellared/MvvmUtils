using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Contracts
{
    public class ArgumentValidator<T>
    {
        private readonly Argument<T> argument;

        internal Argument<T> Argument { get { return argument; } }

        public ArgumentValidator(Argument<T> argument)
        {
            this.argument = argument;
        }

        public ArgumentValidator<T> Is<TException>(Func<T, bool> condition, Func<Argument<T>, TException> exceptionDelegate)
            where TException : ArgumentException
        {
            if (!condition(Argument.Value))
            {
                throw exceptionDelegate(argument);
            }

            return this;
        }

        public ArgumentValidator<T> Is(Func<T, bool> condition, string conditionDescription = "")
        {
            return Is(condition, arg => new ArgumentException(conditionDescription, arg.Name));
        }

        /// <summary>
        /// Returns formated condition description.
        /// </summary>
        /// <param name="format">{0} for argument's name, {1} for argument's value</param>
        /// <returns>Formated condition description</returns>
        public string BuildConditionDescription(string format)
        {
            return string.Format(format, Argument.Name, Argument.Value);
        }
    }
}