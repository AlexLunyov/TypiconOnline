﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    
    public class KanonasItemBusinessConstraint
    {
        public static readonly BusinessConstraint SourceRequired = new BusinessConstraint("Отсутствуют определение источника песнопения (книги).");
        public static readonly BusinessConstraint PlaceRequired = new BusinessConstraint("Отсутствуют определение места в источнике песнопения (книги).");
        public static readonly BusinessConstraint CountInvalid = new BusinessConstraint("Определение количества тропарей должно быть больше нуля.");
        public static readonly BusinessConstraint IrmosCountInvalid = new BusinessConstraint("Определение количества ирмосов не может принимать отрицательное значение.");
    }
}
