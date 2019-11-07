using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Rules.Variables
{
    public abstract class Variable<T> where T: class
    {
        private T _value;

        public Variable(string value)
        {
            if (IsVariable(value))
            {
                VariableName = value.Replace("[", "").Replace("]", "");
                HasValue = false;
            }
            else
            {
                Value = Initiate(value);
                HasValue = true;
            }
        }

        private bool IsVariable(string value) => value.StartsWith("[") && value.EndsWith("]");

        public bool HasValue { get; }

        public T Value
        {
            get
            {
                return (HasValue) ? _value : throw new InvalidOperationException();
            }
            set 
            {
                _value = value;
            }
        }

        public string VariableName { get; }

        protected abstract T Initiate(string value);
    }
}
