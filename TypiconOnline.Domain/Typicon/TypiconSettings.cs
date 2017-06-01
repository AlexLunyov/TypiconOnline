using System;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconSettings : EntityBase<int>
    {
        public virtual TypiconEntity TypiconEntity { get; set; }

        public virtual string DefaultLanguage { get; set; }

        private Sign _templateSunday;

        public virtual Sign TemplateSunday
        {
            get
            {
                //TODO: реализовать покрасивей
                //должен быть добавлен признак IsTemplateSunday в Sign
                if (_templateSunday == null)
                {
                    _templateSunday = TypiconEntity.Signs.Find(c => c.Number == 6);
                }
                return _templateSunday;
            }
            set
            {
                _templateSunday = value;
            }
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}