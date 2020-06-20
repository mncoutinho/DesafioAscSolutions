namespace DesafioASCSolutions.Models
{
    public class Aluno 
    {
        public int Id {get;set;}

        public string Name {get;set;}
        public double? Nota1{get;set;}
        public double? Nota2{get;set;}
        public double? Nota3{get;set;}
        public double? Media{get;set;}
        public double? MediaFinal{get;set;}
        public string StatusAluno{get;set;}
        public int IdTurma {get;set;}

       
    }
}