using CloudPayments.Services;

namespace CloudPayments.Models
{
    public class ProductViewModel
    {
        private readonly Product _product;
        private readonly ICurrencyManager _currencyManager;
        public int Id => _product.Id;
        public string Title => _product.Title;

        public double Price => _product.Price;
        public string ImagePath => "/images/" + _product.ImageName;
        public string Currency => _currencyManager.ParseCurrency(_product.Currency).Code;
        public string ViewCurrency => _product.Currency;

        public ProductViewModel(Product product, ICurrencyManager currencyManager)
        {
            _product = product;
            _currencyManager = currencyManager;
        }
    }
}