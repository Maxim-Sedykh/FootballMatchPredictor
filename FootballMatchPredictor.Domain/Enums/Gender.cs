using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Enums
{
    public enum Gender
    {
        [Display(Name = "Женщина")]
        Woman = 0,

        [Display(Name = "Мужчина")]
        Man = 1,
    }
}
