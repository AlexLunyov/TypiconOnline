using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class KanonasRuleVMFactory : ViewModelFactoryBase<KanonasRule>
    {
        public KanonasRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
        }

        public override void Create(CreateViewModelRequest<KanonasRule> req)
        {
            //добавляем шапку
            AppendHeader();

            //находим все песни и действия после них
            for (int i = 1; i <= 9; i++)
            {
                CombineOdi(i);
                AppendAterRule(i);
            }
        }

        private void AppendHeader()
        {
            throw new NotImplementedException();
        }

        private void CombineOdi(int i)
        {
            throw new NotImplementedException();
        }

        private void AppendAterRule(int i)
        {
            throw new NotImplementedException();
        }
    }
}
