namespace BedeThirteen.Services.SerializationModels
{
    using System.Collections.Generic;

    public class ExchangeRates
    {
        public string Date { get; set; }

        public IDictionary<string, decimal> Rates { get; set; }

        public string Base { get; set; }
    }
}
