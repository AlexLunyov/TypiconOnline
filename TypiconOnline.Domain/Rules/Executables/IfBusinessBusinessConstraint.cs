﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class IfBusinessBusinessConstraint
    {
        public static readonly BusinessConstraint ConditionRequired = new BusinessConstraint("Должно быть определено условие.");
        public static readonly BusinessConstraint ThenRequired = new BusinessConstraint("Должно быть определено действие, в случае удовлетворения условию.");
    }
}
