using CarService.Server.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Features.ShopInterface.Model
{
    public class Procedure : ProcedureBase
    {
        private Procedure() { }

        public Procedure(string name, string color)
        {
            if (!long.TryParse(color, System.Globalization.NumberStyles.HexNumber, null, out long colorCode))
            {
                throw new ArgumentException($"Color code with value {color} is invalid: the color code needs to be a valid hexadecimal code.");
            }

            if (colorCode < 0 || colorCode > 16777215)
            {
                throw new ArgumentException($"Color code with value {color} is invalid: the color code can have a minimum value of 0 and a maximum value of FFFFFF.");
            }

            Name = name;
            Color = color;
        }
    }
}
