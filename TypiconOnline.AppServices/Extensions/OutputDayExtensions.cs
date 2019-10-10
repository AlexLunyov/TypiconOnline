using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon.Output;

namespace TypiconOnline.AppServices.Extensions
{
    public static class OutputDayExtensions
    {
        public static void AddWorship(this OutputDay day, OutputWorshipModel worshipModel, ITypiconSerializer typiconSerializer)
        {
            var w = new OutputWorship()
            {
                OutputDay = day,
                Time = worshipModel.Time,
                Name = new ItemTextStyled(worshipModel.Name),
                AdditionalName = (worshipModel.AdditionalName != null) ?  new ItemText(worshipModel.AdditionalName) : new ItemText(),
                Definition = typiconSerializer.Serialize(worshipModel.ChildElements)
            };
            day.Worships.Add(w);
        }

        public static void AddWorships(this OutputDay day, List<OutputWorshipModel> worshipModels, ITypiconSerializer typiconSerializer)
        {
            foreach (var wm in worshipModels)
            {
                day.AddWorship(wm, typiconSerializer);
            }
        }
    }
}
