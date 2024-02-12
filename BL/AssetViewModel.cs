using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BL
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }


        public string MimeType { get; set; }
        public string FileExtention { get; set; }

        public List<Guid> ProductIds { get; set; }




        public AssetViewModel()
        {

        }
        public AssetViewModel(Asset asset)
        {
            Id = asset.Id;
            FileName = asset.FileName;
            OriginalFileName = asset.OriginalFileName;
            MimeType = asset.MimeType;
            FileExtention = asset.MimeType;
            ProductIds = asset.Products.Select(p => p.Id).ToList();
        }

        public static implicit operator Asset(AssetViewModel model)
        {
            return new Asset
            {

                Id = model.Id,
                FileName = model.FileName,
                OriginalFileName = model.OriginalFileName,
                MimeType = model.MimeType,
                FileExtention = model.MimeType,
            };
        }

    }
}
