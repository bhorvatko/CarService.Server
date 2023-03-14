using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Procedure : Entity
    {
        public string Name { get; protected set; }
        public string Color { get; protected set; }
        public IEnumerable<Step> Steps { get; protected set; } = new List<Step>();

#nullable disable warnings 
        private Procedure() { }
#nullable enable warnings 

        public Procedure(string name, string color)
        {
            Update(name, color);
        }

        [MemberNotNull(nameof(Name))]
        [MemberNotNull(nameof(Color))]
        public void Update(string name, string color)
        {
            ValidateColor(color);

            Name = name;
            Color = color;
        }

        public void ValidateColor(string color)
        {
            if (!long.TryParse(color, System.Globalization.NumberStyles.HexNumber, null, out long colorCode))
            {
                throw new ArgumentException($"Color code with value {color} is invalid: the color code needs to be a valid hexadecimal code.");
            }

            if (colorCode < 0 || colorCode > 16777215)
            {
                throw new ArgumentException($"Color code with value {color} is invalid: the color code can have a minimum value of 0 and a maximum value of FFFFFF.");
            }
        }
    }
}
