using System.ComponentModel.DataAnnotations;

namespace CarService.Server.DataModel
{
    public class ProcedureBase : Entity
    {
        public string Name { get; protected set; } = string.Empty;
        public string Color { get; protected set; } = string.Empty;
    }
}