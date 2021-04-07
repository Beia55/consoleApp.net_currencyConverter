using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CurrencyConvertorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Type, object> dependecyInjectionContainer = new Dictionary<Type, object>();
                dependecyInjectionContainer.Add(typeof(IINGExchangeRate), new INGExchangeRate2020());
                dependecyInjectionContainer.Add(typeof(IBTExchangeRate), new BTExchangeRate2020());
                dependecyInjectionContainer.Add(typeof(IRFSExchangeRate), new RFSExchangeRate2020());
                dependecyInjectionContainer.Add(typeof(IBPOSExchangeRate), new BPOSExchangeRate2020());

            ConstructorInfo constructorInfo = typeof(ING).GetConstructors().FirstOrDefault();

            List<object> ingParams = new List<object>();

            foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
            {
                ingParams.Add(dependecyInjectionContainer[parameterInfo.ParameterType]);
            }

            var ingCtorParams = ingParams.ToArray();

            IBank ing = Activator.CreateInstance(typeof(ING), ingCtorParams) as ING;
            IBank bt = new BT(dependecyInjectionContainer[typeof(IBTExchangeRate)] as IBTExchangeRate);
            IBank rfs = new RFS(dependecyInjectionContainer[typeof(IRFSExchangeRate)] as IRFSExchangeRate);
            IBank bpos = new BPOS(dependecyInjectionContainer[typeof(IBPOSExchangeRate)] as IBPOSExchangeRate);


            Console.WriteLine($"ING converts 100 EUR into {ing.ExchangeInRON(100)}");
            Console.WriteLine($"BT converts 100 EUR into {bt.ExchangeInRON(100)}");
            Console.WriteLine($"RFS converts 100 EUR into {rfs.ExchangeInRON(100)}");
            Console.WriteLine($"BPOS converts 100 EUR into {bpos.ExchangeInRON(100)}");

            Console.ReadKey();
        }
    }

    // fiecare banca are propriul schimb valutar
    // ING: 1 EUR = 4.8554 RON
    // BT: 1 EUR = 4.7554 RON
    // RAIFFEISEN -> RFS: 1 EUR = 4.8565 RON
    // BancPost -> BPOS: 1 EUR = 4.7750 RON
    // GOAL: Create banks that exchange from EUR to RON currency

    public interface IBank // ING, BT
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