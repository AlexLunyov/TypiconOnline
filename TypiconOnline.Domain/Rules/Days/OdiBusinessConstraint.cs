﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    public class OdiBusinessConstraint
    {
        public static readonly BusinessConstraint InvalidNumber = new BusinessConstraint("Неверный номер песни (допустимые значения с 1 до 9).");
        public static readonly BusinessConstraint IrmosRequired = new BusinessConstraint("Ирмос канона обязателен для заполнения.");
        public static readonly BusinessConstraint TroparionRequired = new BusinessConstraint("Должен быть определен хотя бы один тропарь в песне.");
    }
}
