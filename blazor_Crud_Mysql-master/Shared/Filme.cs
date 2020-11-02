using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace blazor_mysql2.Shared
{
    public class Filme
    {
        public int Id { get; set; }
        [Required]
        public string Genero { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? Date { get; set; }
    }
}
