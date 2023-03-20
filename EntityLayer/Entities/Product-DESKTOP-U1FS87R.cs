using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLayer.Entities
{
   public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Max 500 karakter olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Popüler")]
        public bool Popular { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Onay")]
        public bool IsApproved { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Stok")]
        public int Stock { get; set; }
      
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public int? ProductCouponCode { get; set; }
    
        public virtual Category Category { get; set; }

        public virtual List<Sales> Sales { get; set; }
      
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Coupon> Coupons { get; set; }




    }
}
