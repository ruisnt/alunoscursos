﻿namespace InscricaoAluno.Api.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public bool Excluido { get; set; }
    }
}
