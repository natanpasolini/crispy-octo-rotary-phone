using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaConsultasUVV.Models;

public class Consulta
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "A especialidade é obrigatória.")]
    [StringLength(80, ErrorMessage = "A especialidade pode conter até 80 caracteres.")]
    [Display(Name = "Especialidade Médica")]
    public string Especialidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data e hora são obrigatórias.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Data e Horário")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição pode ter até 500 caracteres.")]
    [Display(Name = "Descrição / Motivo")]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int UsuarioId { get; set; }

    [ForeignKey(nameof(UsuarioId))]
    public Usuario? Usuario { get; set; }
}
