﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class CaseBusinessConstraint
    {
        public static readonly BusinessConstraint ValuesRequired = new BusinessConstraint("Должно быть определено хотя бы одно значение.");
        public static readonly BusinessConstraint ValuesTypeMismatch = new BusinessConstraint("Значения должны быть одного типа.");
        public static readonly BusinessConstraint ActionsRequired = new BusinessConstraint("Должно быть определено действие.");
    }
}
