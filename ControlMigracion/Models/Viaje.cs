using System;
using System.ComponentModel.DataAnnotations;

namespace ControlMigracion.Models
{
    public class Viaje
    {
        public int ViajeID { get; set; }

        [Required(ErrorMessage = "El ID del viajero es requerido")]
        public int ViajeroID { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "El destino es requerido")]
        [StringLength(100, ErrorMessage = "El destino no puede tener más de 100 caracteres")]
        public string Destino { get; set; }

        public virtual Viajero Viajero { get; set; }
    }
}

