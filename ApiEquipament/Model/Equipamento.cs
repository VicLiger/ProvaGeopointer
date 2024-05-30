using System.ComponentModel.DataAnnotations;


namespace ApiEquipament.Model 
{ 
    public class Equimento { 


        [Key] 
        public int Id { get; set; } 

        [Required]
        [StringLength(50, ErrorMessage = "A tag deve ter no máximo 50 caracteres")]
        public string Tag { get; set; } 
        
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")] 
        public string Name { get; set; } 
        
        [Required]
        [Url(ErrorMessage = "O campo deve ser uma url válida")] 
        public string File { get; set; } // É O ESTADO DE CONSTRUÇÃO COM RELAÇÃO A MAQUETE[Required][StringLength(200, ErrorMessage = "O estado deve ter tamanho máximo de 200 caracteres")]public string State { get; set; }}}