using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;

namespace TypiconOnline.AppServices.Migration
{
    public class KatavasiaMigrationManager : IMigrationManager
    {
        public IKatavasiaFactory Factory { get; private set; }
        public IFileReader FileReader { get; private set; }
        public IKatavasiaService Service { get; private set; }

        public KatavasiaMigrationManager(IKatavasiaFactory factory,
                                                    IFileReader fileReader,
                                                    IKatavasiaService service)
        {
            Factory = factory ?? throw new ArgumentNullException("factory");
            FileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            Service = service ?? throw new ArgumentNullException("service");
        }

        public void Import()
        {
            AddKatavasiaRequest request = new AddKatavasiaRequest();

            foreach ((string name, string content) in FileReader.ReadAllFromDirectory())
            {
                request.Katavasia = Factory.Create(name, content);

                Service.Insert(request);
            }
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
