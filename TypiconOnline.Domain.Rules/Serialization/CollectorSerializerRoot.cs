﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Serialization
{
    /// <summary>
    /// Обертка для базового класса. Нужен для определения Переменных в десериализуемом Правиле
    /// </summary>
    public class CollectorSerializerRoot : RuleSerializerRoot
    {
        private List<IHavingVariables> _collection = new List<IHavingVariables>();
        public CollectorSerializerRoot([NotNull] IQueryProcessor queryProcessor, ITypiconSerializer typiconSerializer) : base(queryProcessor, typiconSerializer)
        {
        }

        public override IRuleSerializerContainer<T> Container<T>()
        {
            return new CollectorContainer<T>(base.Container<T>(), this);
        }

        public void AddHavingVariablesElement(IHavingVariables element)
        {
            _collection.Add(element);
        }

        public IEnumerable<IHavingVariables> GetHavingVariables() => _collection;
    }
}
