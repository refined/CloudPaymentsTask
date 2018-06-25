using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CloudPayments.Services;

namespace CloudPayments.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public double Price { get; set; }
        public string ImageName { get; set; }
        public string Currency { get; set; }
    }

    public class ProductViewModel
    {
        private readonly Product _product;
        private readonly ICurrencyManager _currencyManager;
        public int Id => _product.Id;
        public string Title => _product.Title;

        public double Price => _product.Price;
        public string ImagePath => "/images/" + _product.ImageName;
        public string Currency => _currencyManager.ParseCurrency(_product.Currency).Code;

        public ProductViewModel(Product product, ICurrencyManager currencyManager)
        {
            _product = product;
            _currencyManager = currencyManager;
        }
    }
}
