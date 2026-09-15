using System.ComponentModel.DataAnnotations;

namespace SistemaConsultasUVV.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    [Display(Name = "Nome Completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    [StringLength(150)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(255)]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    [DataType(DataType.DateTime)]
    [Display(Name = "Data de Cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}
