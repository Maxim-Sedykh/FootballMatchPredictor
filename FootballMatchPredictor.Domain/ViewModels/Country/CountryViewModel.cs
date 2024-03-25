using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.Country
{
    public class CountryViewModel
    {
        public CountryName Name { get; set; }
    }

    public class CountryName
    {
        public string Common { get; set; }

        public string Official { get; set; }
    }
}
