using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc.Html;

namespace OpenOrderFramework.Models
{
    [Bind(Exclude = "ID")]
    public class Item
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Kategori")]
        public int CatagorieId { get; set; }

        [Required(ErrorMessage = "Nama Produk Belum Diisi !")]
        [StringLength(160)]
        public string Name { get; set; }



        [Required(ErrorMessage = "Harga Produk Belum Diisi !")]
        [Range(1, 99999999999, ErrorMessage = "Harga Harus Dari 1 - 99999999999 !")]
        public decimal Price { get; set; }

        public byte[] InternalImage { get; set; }

        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }

            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();

                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        [DisplayName("Foto Produk")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }

        public virtual Catagorie Catagorie { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}