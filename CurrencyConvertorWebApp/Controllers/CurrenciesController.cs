using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConvertorWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        private readonly IBank bank;
        public CurrenciesController(IBank bank) 
        {
            this.bank = bank;
        }

        [HttpGet]
        public decimal ExchangeInRON(decimal valueInEUR) 
        {
            return bank.ExchangeInRON(valueInEUR);
        }
    }

    public interface IBank // ING, BT, RSF, BPOS
    {
        decimal ExchangeInRON(decimal valueInEUR);
    }

    public interface IINGExchangeRate
    {
        decimal Value();
    }

    public interface IBTExchangeRate
    {
        decimal Value();
    }

    public interface IRFSExchangeRate
    {
        decimal Value();
    }

    public interface IBPOSExchangeRate
    {
        decimal Value();
    }

    public class INGExchangeRate2020 : IINGExchangeRate
    {
        public decimal Value() => 4.8554M;
    }

    public class INGExchangeRate2021 : IINGExchangeRate
    {
        public decimal Value() => 4.86M;
    }

    public class BTExchangeRate2020 : IBTExchangeRate
    {
        public decimal Value() => 4.7554M;
    }

    public class BTExchangeRate2021 : IBTExchangeRate
    {
        public decimal Value() => 4.76M;
    }

    public class RFSExchangeRate2020 : IRFSExchangeRate
    {
        public decimal Value() => 4.8565M;
    }

    public class RFSExchangeRate2021 : IRFSExchangeRate
    {
        public decimal Value() => 4.86M;
    }

    public class BPOSExchangeRate2020 : IBPOSExchangeRate
    {
        public decimal Value() => 4.7750M;
    }

    public class BPOSExchangeRate2021 : IBPOSExchangeRate
    {
        public decimal Value() => 4.78M;
    }

    public class ING : IBank
    {
        private readonly IINGExchangeRate iNGExchangeRate;

        public ING(IINGExchangeRate iNGExchangeRate)
        {
            this.iNGExchangeRate = iNGExchangeRate;
        }

        public decimal ExchangeInRON(decimal valueInEUR)
        {
            return valueInEUR * iNGExchangeRate.Value();
        }
    }

    public class BT : IBank
    {
        private readonly IBTExchangeRate bTExchangeRate;

        public BT(IBTExchangeRate bTExchangeRate)
        {
            this.bTExchangeRate = bTExchangeRate;
        }

        public decimal ExchangeInRON(decimal valueInEUR)
        {
            return valueInEUR * bTExchangeRate.Value();
        }
    }

    public class RFS : IBank
    {
        private readonly IRFSExchangeRate rFSExchangeRate;

        public RFS(IRFSExchangeRate rFSExchangeRate)
        {
            this.rFSExchangeRate = rFSExchangeRate;
        }

        public decimal ExchangeInRON(decimal valueInEUR)
        {
            return valueInEUR * rFSExchangeRate.Value();
        }
    }

    public class BPOS : IBank
    {
        private readonly IBPOSExchangeRate bPOSExchangeRate;

        public BPOS(IBPOSExchangeRate bPOSExchangeRate)
        {
            this.bPOSExchangeRate = bPOSExchangeRate;
        }

        public decimal ExchangeInRON(decimal valueInEUR)
        {
            return valueInEUR * bPOSExchangeRate.Value();
        }
    }

}
