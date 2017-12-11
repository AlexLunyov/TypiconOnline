using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.WinForms
{
    public partial class CustomRuleViewer : Form
    {
        TypiconEntity typiconEntity;
        IScheduleService scheduleService;
        CustomRuleSettingsFactory settingsFactory;

        public CustomRuleViewer()
        {
            InitializeComponent();

            InitializeIoC();
        }

        private void InitializeIoC()
        {
            var container = new RegisterByContainer().Container;

            var unitOfWork = container.GetInstance<IUnitOfWork>();

            var typiconEntityService = container.With(unitOfWork).GetInstance<ITypiconEntityService>();

            GetTypiconEntityResponse response = typiconEntityService.GetTypiconEntity(1);// _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            typiconEntity = response.TypiconEntity;

            BookStorage.Instance = new BookStorage(
                container.With(unitOfWork).GetInstance<IEvangelionContext>(),
                container.With(unitOfWork).GetInstance<IApostolContext>(),
                container.With(unitOfWork).GetInstance<IOldTestamentContext>(),
                container.With(unitOfWork).GetInstance<IPsalterContext>(),
                container.With(unitOfWork).GetInstance<IOktoikhContext>(),
                container.With(unitOfWork).GetInstance<ITheotokionAppContext>(),
                container.With(unitOfWork).GetInstance<IEasterContext>(),
                container.With(unitOfWork).GetInstance<IKatavasiaContext>());

            IRuleSerializerRoot serializerRoot = container.With(BookStorage.Instance).GetInstance<IRuleSerializerRoot>();
            settingsFactory = new CustomRuleSettingsFactory();

            scheduleService = container.With(settingsFactory).With(serializerRoot).GetInstance<IScheduleService>();

        }

        private void btnCustomRule_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomRule.Text)) return;

            settingsFactory.CustomRule = txtCustomRule.Text;

            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = monthCalendarCustomRule.SelectionStart,
                Typicon = typiconEntity,
                Handler = new ServiceSequenceHandler(),
                Language = "cs-ru",
                //ApplyParameters = CustomParameters,
                //CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.All)
            };

            request.Handler.Settings.Language = LanguageSettingsFactory.Create("cs-ru");

            GetScheduleDayResponse dayResponse = scheduleService.GetScheduleDay(request);

            TextScheduleDayViewer viewer = new TextScheduleDayViewer();
            viewer.Execute(dayResponse.Day);

            webCustomRule.DocumentText = viewer.GetResult();
        }
    }
}
