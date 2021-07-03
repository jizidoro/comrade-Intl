﻿#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using comrade.Domain.Bases;

#endregion

namespace comrade.Domain.Models
{
    [Table("AIRP_AIRPLANE")]
    public class Airplane : Entity
    {
        [Column("AIRP_TX_CODIGO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Column("AIRP_TX_MODELO", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Column("AIRP_QT_PASSAGEIRO", TypeName = "int")]
        [Required(ErrorMessage = "QuantidadePassageiro is required")]
        public int QuantidadePassageiro { get; set; }

        [Column("AIRP_DT_REGISTRO", TypeName = "varchar")]
        [Required(ErrorMessage = "RegisterDate is required")]
        public DateTime RegisterDate { get; set; }

        public override string Value => Code;
    }
}