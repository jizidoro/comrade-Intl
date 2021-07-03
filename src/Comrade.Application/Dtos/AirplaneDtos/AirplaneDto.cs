#region

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos.AirplaneDtos
{
    public class AirplaneDto : EntityDto
    {
        public int Id { get; set; }

        [DisplayName("Code")]
        [Required(ErrorMessage = "Please enter a Code")]
        public string Code { get; set; }

        public string Model { get; set; }
        public int QuantidadePassageiro { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}