using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Тестовый пример просмотрщика последовательности богослужения
    /// </summary>
    public class BypassSequenceViewer : ISequenceViewer
    {
        private IUnitOfWork _unitOfWork;

        public BypassSequenceViewer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
        }
        //public GetSequenceResponse GetSequence(GetSequenceRequest request)
        //{
        //    //TODO: добавить нормальную тестовую строку
        //    string html = @"<!DOCTYPE html>
        //                    <html>
        //                        <head>
        //                            <meta charset=""utf-8"" />
        //                        </head>
        //                        <body>
        //                            <div id=""schberlukiru"">
        //                                <div class=""schedule""><h4 class=""subtitle"">СЕДМИЦА 20-АЯ ПО ПЯТИДЕСЯТНИЦЕ</h4><div style=""margin - top:10px; "">[sign cat=""2""]<strong>16 октября 2017 г.<br/>ПОНЕДЕЛЬНИК<br/>Сщмч. Диони́сия Ареопаги́та, еп. Афи́нского.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>17 октября 2017 г.<br/>ВТОРНИК<br/>Обре́тение мощей свтт. Гу́рия, архиеп. Казанского, и Варсоно́фия, еп. Тверского.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>18 октября 2017 г.<br/>СРЕДА<br/>Свтт. Петра, Алекси́я, Ионы, Макария, Филиппа, Иова, Ермогена, Тихона, Петра, Филарета, Иннокентия и Макария, Московских и всея России чудотворцев.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>19 октября 2017 г.<br/>ЧЕТВЕРГ<br/>Апостола Фомы́.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час.  Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""2""]<strong>20 октября 2017 г.<br/>ПЯТНИЦА<br/>Мчч. Се́ргия и Ва́кха.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час.  Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""1""]<strong>21 октября 2017 г.<br/>СУББОТА<br/>Прп. Пелагии.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>16.00&nbsp;</td><td>Всенощное бдение.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""9""]<strong><span style=""color: #ff0000;"">22 октября 2017 г.<br/>ВОСКРЕСЕНЬЕ<br/>Неделя 20-ая по Пятидесятнице. Глас 3-й. Ап. Иа́кова Алфе́ева.</strong></span></div><table border=0><tr><td>08.40&nbsp;</td><td>Часы 3-й и 6-й.</td></tr><tr><td>09.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table></div><div class=""schedule""><h4 class=""subtitle"">СЕДМИЦА 21-АЯ ПО ПЯТИДЕСЯТНИЦЕ</h4><div style=""margin - top:10px; "">[sign cat=""3""]<strong>23 октября 2017 г.<br/>ПОНЕДЕЛЬНИК<br/>Прп. Амвро́сия О́птинского.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>24 октября 2017 г.<br/>ВТОРНИК<br/>Собор преподобных О́птинских старцев.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час.  Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""1""]<strong>25 октября 2017 г.<br/>СРЕДА<br/>Мчч. Про́ва, Тара́ха и Андрони́ка. Прп. Космы́, еп. Маиу́мского, творца канонов. </strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>26 октября 2017 г.<br/>ЧЕТВЕРГ<br/>Иверской иконы Божией Матери.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>27 октября 2017 г.<br/>ПЯТНИЦА<br/>Прп. Нико́лы Свято́ши, кн. Черни́говского.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час. Вечерня. Малое повечерие.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""3""]<strong>28 октября 2017 г.<br/>СУББОТА<br/>Свт. Афана́сия исп., еп. Ковро́вского.</strong></div><table border=0><tr><td>06.00&nbsp;</td><td>Полунощница. Утреня. Часы.</td></tr><tr><td>08.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>16.00&nbsp;</td><td>Всенощное бдение.</td></tr></table><div style=""margin - top:10px; "">[sign cat=""9""]<strong><span style=""color: #ff0000;"">29 октября 2017 г.<br/>ВОСКРЕСЕНЬЕ<br/>Неделя 21-ая по Пятидесятнице. Глас 4-й. Мч. Ло́нгина со́тника, иже при Кресте́ Госпо́дни.</strong></span></div><table border=0><tr><td>08.40&nbsp;</td><td>Часы 3-й и 6-й.</td></tr><tr><td>09.00&nbsp;</td><td>Божественная литургия.</td></tr><tr><td>17.30&nbsp;</td><td>9-й час.  Вечерня. Малое повечерие.</td></tr></table></div>
        //                            </div>
        //                        </body>
        //                    </html>";

        //    GetSequenceResponse response = new GetSequenceResponse() { Sequence = html };

        //    return response;
        //}

        public GetSequenceResponse GetSequence(GetSequenceRequest request)
        {
            TypiconEntityService typService = new TypiconEntityService(_unitOfWork);

            GetTypiconEntityResponse resp = typService.GetTypiconEntity(request.TypiconId);

            TypiconEntity typicon = resp.TypiconEntity ?? throw new NullReferenceException("TypiconEntity");

            GetScheduleWeekResponse weekResponse = CreateScheduleService().GetScheduleWeek(new GetScheduleWeekRequest()
            {
                Date = request.Date,
                Typicon = typicon,
                Handler = new ScheduleHandler(),
                Language = "cs-ru",
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            });

            //_unitOfWork.SaveChanges();

            HtmlInnerScheduleWeekViewer viewer = new HtmlInnerScheduleWeekViewer();

            viewer.Execute(weekResponse.Week);

            GetSequenceResponse response = new GetSequenceResponse() { Sequence = viewer.ResultString };

            return response;
        }

        private ScheduleService CreateScheduleService()
        {
            var easters = new EasterContext(_unitOfWork);

            var oktoikhContext = new OktoikhContext(_unitOfWork, easters);

            BookStorage bookStorage = new BookStorage(new EvangelionContext(_unitOfWork),
                                    new ApostolContext(_unitOfWork),
                                    new OldTestamentContext(_unitOfWork),
                                    new PsalterContext(_unitOfWork),
                                    oktoikhContext,
                                    new TheotokionAppContext(_unitOfWork),
                                    easters,
                                    new KatavasiaContext(_unitOfWork),
                                    new WeekDayAppContext(_unitOfWork));

            IRuleSerializerRoot serializerRoot = new RuleSerializerRoot(bookStorage);

            return new ScheduleService(new RuleHandlerSettingsFactory(serializerRoot, new ModifiedRuleService(_unitOfWork))
                                     , new ScheduleDayNameComposer(oktoikhContext));
        }
    }
}
