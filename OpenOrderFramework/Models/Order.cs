using OpenOrderFramework.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Models
{
    [Bind(Exclude = "ID Pesanan")]
    public partial class Order
    {

        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nama Belum Diisi !")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nama Belum Diisi !")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Alamat Belum Diisi !")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Kota Belum Diisi !")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "Provinsi Belum Diisi !")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Kode Pos Belum Diisi !")]
        [DisplayName("Kode Pos")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Negara Belum Diisi !")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "No Telp Belum Diisi !")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Display(Name = "Tanggal Kadaluarsa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Experation { get; set; }

        [Display(Name = "Kartu Kredit")]
        [NotMapped]
        [Required]
        [DataType(DataType.CreditCard)]
        public String CreditCard { get; set; }

        [Display(Name = "Tipe Kartu Kredit")]
        [NotMapped]
        public String CcType { get; set; }

        public bool SaveInfo { get; set; }


        [DisplayName("Alamat Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email Tidak Valid !")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

       

        public string ToString(Order order)
        {
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Informasi Pesanan : "+ order.OrderId +"<br> Pada Tanggal : " + order.OrderDate +"</p>").AppendLine();
            bob.Append("<p>Nama : " + order.FirstName + " " + order.LastName + "<br>");
            bob.Append("Alamat : " + order.Address + " " + order.City + " " + order.State + " " + order.PostalCode + "<br>");
            bob.Append("Kontak : " + order.Email + "     " + order.Phone + "</p>");
            bob.Append("<p>Pembayaran : " + order.CreditCard + " " + order.Experation.ToString("dd-MM-yyyy") + "</p>");
            bob.Append("<p>Kartu Kredit : " + order.CcType + "</p>");

            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Nama Produk</th>" + "<th>Quantity</th>" + "<th>Harga</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + item.Item.Name + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "Belum memesan apapun !";
            }
            bob.Append("</Table>");
            bob.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", order.Total);
            bob.Append(footer).AppendLine();
            bob.Append("</b>");

            return bob.ToString();
        }
    }
}