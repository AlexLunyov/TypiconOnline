using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Variables
{
    public class VariableWorshipRule : IncludingRulesElement, ICustomInterpreted, IHavingVariables
    {
        public VariableWorshipRule(IRuleSerializerRoot serializerRoot)
            : base(string.Empty, serializerRoot)
        {
        }

        public string VariableName { get; set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this))
            {
                //находим переменную
                var query = SerializerRoot.QueryProcessor.Process(new TypiconVariableQuery(handler.Settings.TypiconVersionId, VariableName));

                //если найдена и у нее определено Значение
                if (query.Value is TypiconVariable var
                    && !string.IsNullOrEmpty(var.Value))
                {
                    //десериализуем
                    var container = SerializerRoot.Container<ExecContainer>().Deserialize(var.Value);

                    if (container != null)
                    {
                        //находим все службы
                        foreach (var worship in container.GetChildElements<WorshipRule>(handler.Settings))
                        {
                            //обрабатываем их
                            handler.Execute(worship);
                        }
                    }
                }
            }
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(VariableName))
            {
                AddBrokenConstraint(CommonRuleBusinessConstraint.NameReqiured);
            }
        }


        #region IHavingVariables implementation

        public IEnumerable<(string Name, VariableType Type)> GetVariableNames()
        {
            return new[] { (VariableName, VariableType.Worship) };
        }

        #endregion
    }
}
