﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    public class KanonasBusinessConstraint
    {
        public static readonly BusinessConstraint NameRequired = new BusinessConstraint("Необходимо наличие наименования канона.");
        public static readonly BusinessConstraint OdiRequired = new BusinessConstraint("Необходимо наличие хотя бы одной песни канона.");
    }
}
