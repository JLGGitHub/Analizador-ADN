using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Adn")]
    public class Adn
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AdnChain { get; set; }
        [Required]
        public bool Mutant { get; set; }
    }
}
