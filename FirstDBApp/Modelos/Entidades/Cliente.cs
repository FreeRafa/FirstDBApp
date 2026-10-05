using System;
using System.Collections.Generic;
using System.Text;

namespace FirstDBApp.Modelos.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateOnly CriadoEm { get; set; }
        public bool EstaAtivo { get; set; }
    }
}
