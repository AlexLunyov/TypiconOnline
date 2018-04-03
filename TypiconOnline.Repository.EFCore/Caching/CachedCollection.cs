using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Repository.EFCore.Caching
{
    public abstract class CachedCollection<U>
    {
        protected Dictionary<string, U> pointers = new Dictionary<string, U>();

        protected TimeSpan DurationTime { get; }

        public CachedCollection(TimeSpan durationTime)
        {
            this.DurationTime = durationTime;
        }

        public void AddPointer(string key, U value)
        {
            if (pointers.ContainsKey(key))
            {
                pointers[key] = value;
            }
            else
            {
                pointers.Add(key, value);
            }
        }

        public void RemovePointer(string key)
        {
            if (pointers.ContainsKey(key))
            {
                pointers.Remove(key);
            }
        }

        protected abstract void ClearExpiredPointers();
    }
}
