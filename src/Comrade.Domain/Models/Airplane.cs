#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Domain.Models
{
    [Table("AIRP_AIRPLANE")]
    public class Airplane : Entity
    {
        public Airplane()
        {
            Code = "";
            Model = "";
            PassengerQuantity = 0;
        }

        public Airplane(string code, string model, int passengerQuantity, DateTime registerDate)
        {
            Code = code;
            Model = model;
            PassengerQuantity = passengerQuantity;
            RegisterDate = registerDate;
        }

        [Column("AIRP_TX_CODIGO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Column("AIRP_TX_MODELO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Model is required")]
        public string? Model { get; set; }

        [Column("AIRP_QT_PASSAGEIRO", TypeName = "int")]
        [Required(ErrorMessage = "PassengerQuantity is required")]
        public int PassengerQuantity { get; set; }

        [Column("AIRP_DT_REGISTRO", TypeName = "varchar")]
        [Required(ErrorMessage = "RegisterDate is required")]
        public DateTime RegisterDate { get; set; }

        public override string Value => Code;
    }
}