using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Models;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableEditWorshipModel: VariableEditModelBase
    {
        /// <summary>
        /// Значение переменной в формате, зависимом от типа переменной
        /// </summary>
        public string Value { get; set; }
        //public WorshipContainer Value { get; set; }
        //public List<WorshipModel> Value { get; set; }
    }
}
