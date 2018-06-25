using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudPayments.Services
{
    public abstract class CurrencyCodeBase
    {
        public abstract string Code { get; }
        public abstract List<string> Synonyms { get; }

        public bool IsSuitable(string str)
        {
            return Synonyms.Any(s => s == str);
        }
    }

    public class RubCurrencyCode : CurrencyCodeBase
    {
        public override string Code => "RUB";
        public override List<string> Synonyms => new List<string> {"р", "руб", "rub", "рублей", "рубль", "рубля"};
    }
    public class UsdCurrencyCode : CurrencyCodeBase
    {
        public override string Code => "USD";
        public override List<string> Synonyms => new List<string> { "usd", "dollar", "dollars" };
    }

    public interface ICurrencyManager
    {
        bool CheckCurrency(string str);
        CurrencyCodeBase ParseCurrency(string str);
    }

    public class CurrencyManager : ICurrencyManager
    {
        private readonly List<CurrencyCodeBase> _currencies;
        public CurrencyManager()
        {
            _currencies = new List<CurrencyCodeBase>
            {
                new RubCurrencyCode(), new UsdCurrencyCode()
            };
        }
        
        public bool CheckCurrency(string str)
        {
            return ParseCurrency(str) != null;
        }

        public CurrencyCodeBase ParseCurrency(string str)
        {
            str = str.ToLower().Replace(".", "").Replace(" ", "");
            return _currencies.FirstOrDefault(c => c.IsSuitable(str));
        }
    }

}
