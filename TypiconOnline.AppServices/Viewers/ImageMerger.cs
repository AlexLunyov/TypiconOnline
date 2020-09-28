using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TypiconOnline.AppServices.Viewers
{
    public class ImageMerger
    {
        private readonly IList<ImageInfo> _imageInfosOfTheTargetDocument = new List<ImageInfo>();
        private readonly MainDocumentPart _targetDocumentPart;

        public ImageMerger(WordprocessingDocument targetDocument)
        {
            this._targetDocumentPart = targetDocument.MainDocumentPart;
        }

        public void Merge(WordprocessingDocument sourceDocument)
        {
            MainDocumentPart sourceDocumentPart = sourceDocument.MainDocumentPart;
            IList<ImageInfo> imageInfosOfTheSourceDocument = this.getImageInfos(sourceDocumentPart);
            if (0 == imageInfosOfTheSourceDocument.Count) { return; }

            this.addTheImagesToTheTargetDocument(imageInfosOfTheSourceDocument);
            this.rereferenceTheImagesToTheirCorrespondingImageParts(sourceDocumentPart, imageInfosOfTheSourceDocument);
        }

        private void addTheImagesToTheTargetDocument(IList<ImageInfo> images)
        {
            foreach (var img in images)
            {
                img.Reparent(this._targetDocumentPart);
                this._imageInfosOfTheTargetDocument.Add(img);
            }
        }

        private IList<ImageInfo> getImageInfos(MainDocumentPart documentPart)
        {
            List<ImageInfo> r = new List<ImageInfo>();

            foreach (ImagePart image in documentPart.ImageParts)
            {
                ImageInfo imageInfo = ImageInfo.Create(documentPart, image);
                r.Add(imageInfo);
            }

            return r;
        }

        private void rereferenceTheImagesToTheirCorrespondingImageParts(MainDocumentPart sourceDocumentPart, IList<ImageInfo> imageInfosOfTheSourceDocument)
        {
            IEnumerable<Drawing> images = sourceDocumentPart.Document.Body.Descendants<Drawing>();

            foreach (Drawing image in images)
            {
                Blip blip = image.Inline.Graphic.GraphicData.Descendants<Blip>().FirstOrDefault();
                String originalId = blip.Embed.Value;

                ImageInfo imageInfo = imageInfosOfTheSourceDocument.FirstOrDefault(o => o.OriginalId.Equals(originalId));
                blip.Embed.Value = imageInfo.Id;
            }
        }
    }

    public class ImageInfo
    {
        private String _id;
        private ImagePart _image;
        private readonly String _originalId;

        private ImageInfo(ImagePart image, String id)
        {
            this._id = id;
            this._image = image;
            this._originalId = id;
        }

        public String Id
        {
            get { return this._id; }
        }

        public ImagePart Image
        {
            get { return this._image; }
        }

        public String OriginalId
        {
            get { return this._originalId; }
        }

        public static ImageInfo Create(MainDocumentPart documentPart, ImagePart image)
        {
            String id = documentPart.GetIdOfPart(image);
            ImageInfo r = new ImageInfo(image, id);
            return r;
        }

        public void Reparent(MainDocumentPart documentPart)
        {
            ImagePart newImage = documentPart.AddImagePart(this._image.ContentType);
            newImage.FeedData(this._image.GetStream(FileMode.Open, FileAccess.Read));
            String newId = documentPart.GetIdOfPart(newImage);
            this._id = newId;
            this._image = newImage;
        }
    }
}
