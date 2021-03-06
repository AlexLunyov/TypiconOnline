﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyDayBusinessConstraint
    {
        public static readonly BusinessConstraint DateDoubleDefinition = new BusinessConstraint("Не может быть одновременно определены два параметра перемещения дня: количество дней для переноса и конкретная дата.");
        public static readonly BusinessConstraint EmptyElement = new BusinessConstraint("Пустой элемент.");
        public static readonly BusinessConstraint DateAbsense = new BusinessConstraint("Отсутствует определение даты.");

        //public static readonly BusinessConstraint ServicesSignTypeMismatch = new BusinessConstraint("Значение знака службы должно быть целочисленным.");
        //public static readonly BusinessConstraint IsLastNameTypeMismatch = new BusinessConstraint("Значение должно быть логическим.");
        //public static readonly BusinessConstraint UseFullNameTypeMismatch = new BusinessConstraint("Значение должно быть логическим."); 
        //public static readonly BusinessConstraint DayMoveTypeMismatch = new BusinessConstraint("Значение количества дней для переноса должно быть целочисленным.");
        //public static readonly BusinessConstraint TooMuchChildren = new BusinessConstraint("Не может быть определено более одного дочернего элемента.");
    }
}
