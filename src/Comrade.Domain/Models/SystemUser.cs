#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Domain.Models
{
    [Table("USSI_USUARIO_SISTEMA")]
    public class SystemUser : Entity
    {
        public SystemUser()
        {
            Name = "";
            Password = "";
            Registration = "";
        }

        public SystemUser(int id, string name, string email, string password, string registration,
            DateTime? registerDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Registration = registration;
            RegisterDate = registerDate;
        }

        [Column("USSI_TX_NOME", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "NOME USU is required")]
        public string Name { get; set; } // varchar(255), not null

        [Column("USSI_TX_EMAIL", TypeName = "varchar")]
        [MaxLength(255)]
        public string? Email { get; set; } // varchar(255), null

        [Column("USSI_TX_SENHA", TypeName = "varchar")]
        [MaxLength(1023)]
        [Required(ErrorMessage = "SENHA is required")]
        public string Password { get; set; } // varchar(1023), not null

        [Column("USSI_TX_MATRICULA", TypeName = "varchar")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Registration is required")]
        public string Registration { get; set; } // varchar(255), not null

        [Column("USSI_DT_REGISTRO", TypeName = "varchar")]
        public DateTime? RegisterDate { get; set; }

        public override string Value => Name;
    }
}