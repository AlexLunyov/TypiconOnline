using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Элементарная частичка выходной формы последовательности богослужений
    /// </summary>
    [DataContract]
    public class ViewModelItem 
    {
        [DataMember]
        public string KindStringValue { get; set; }
        [DataMember]
        public ViewModelItemKind Kind { get; set; }
        [DataMember]
        //TODO: необходимо добавить к строковым значениям каждого параграфа также и стиль
        public List<ParagraphViewModel> Paragraphs { get; set; }
    }

    public enum ViewModelItemKind { Choir, Lector, Priest, Deacon, Stihos, Text, Irmos, Troparion, Chorus, Theotokion }
}
