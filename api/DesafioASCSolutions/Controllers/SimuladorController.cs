using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioASCSolutions.Data;
using DesafioASCSolutions.Models;

namespace DesafioASCSolutions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SimuladorController : ControllerBase
    {
        private readonly EscolaContext _context;
        

        public SimuladorController(EscolaContext context)
        {
            _context = context;
        }
        // Get By Turma Url: /Simulador/{TurmaId}
        [HttpGet("{TurmaId}")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunoByTurma(int TurmaId)
        {
            var getTurmaById = _context.Alunos.Where(i => i.IdTurma == TurmaId).ToListAsync();
            var aluno = await getTurmaById;
            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }
           [HttpGet]
           [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno(int TurmaId)
        {
            var getTurmaById = _context.Alunos.ToListAsync();
            var aluno = await getTurmaById;
            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }
               [HttpGet]
           [Route("GetAllClassificados")]
        public async Task<ActionResult<IEnumerable<Premio>>> GetAlunoClassificados(int TurmaId)
        {
            var getTurmaById = _context.Premios.ToListAsync();
            
            var alunos = await getTurmaById;
            foreach (Premio aluno in alunos)
            {
                aluno.Aluno = _context.Alunos.Find(aluno.IdAluno);
            }
            if (alunos == null)
            {
                return NotFound();
            }

            return alunos;
        }
        [HttpGet]
        public async Task<ActionResult<Turma>> Simulador()
        {
            Random r = new Random();
            for(int countTurma = 3; countTurma >= 1; countTurma--){
            Turma turma  = new Turma();
            turma.Name = $"Turma{countTurma}";    
                for(int countAluno = 20; countAluno >= 1; countAluno--)
                {
                    Aluno aluno  = new Aluno();
                    aluno.Name = "Aluno"+countAluno+"."+countTurma;
                    aluno.IdTurma = countTurma;
                    // gera Notas
                    aluno.Nota1 = r.Next(0,10);
                    aluno.Nota2 = r.Next(0,10);
                    aluno.Nota3 = r.Next(0,10);
                    // Calcula Média
                    aluno.Media = (aluno.Nota1 + (aluno.Nota2*1.2)+ (aluno.Nota3*1.4))/3.6;
                    // Regras de Aprovação
                    if(aluno.Media < 4)
                    {
                        aluno.StatusAluno ="Reprovado";
                        aluno.MediaFinal = aluno.Media;
                    }
                    if(aluno.Media < 6 && aluno.Media > 4)
                    {
                        aluno.StatusAluno = "Recuperação";
                        double ProvaFinal = r.Next(0,10);
                        aluno.MediaFinal = (aluno.Media + ProvaFinal)/2.0;
                        if(aluno.MediaFinal >= 5){
                             aluno.StatusAluno = "Aprovado Na Prova Final";
                        }
                        else{
                            aluno.StatusAluno ="Reprovado";
                        }
                    }
                    if(aluno.Media > 6)
                    {
                        aluno.StatusAluno = "Aprovado";
                        aluno.MediaFinal = aluno.Media;
                    }
                    // Adiciona Aluno ao banco
                    _context.Alunos.Add(aluno);
                }
            _context.Turmas.Add(turma);
            }

            await _context.SaveChangesAsync();

            return Ok("Salvo");
        }
        [HttpGet]
        [Route("Premio/Classificados")]
        public async Task<ActionResult<IEnumerable<Aluno>>> Premio()
        {
            var getTurmaById = _context.Alunos.ToListAsync();
            var aluno = await getTurmaById;
            if (aluno == null)
            {
                return NotFound();
            }
            var classificados = aluno.OrderByDescending(i => i.MediaFinal).Take(5).ToList();
            foreach (Aluno classificado in classificados)
            {
                Random r = new Random();
                Premio premio = new Premio();
                premio.Aluno = _context.Alunos.Find(classificado.Id);
                premio.IdAluno = classificado.Id;
                premio.Aluno.StatusAluno = "Classificado para a Prova de Premiação";
                premio.ProvaPremio = r.Next(0,10);
                _context.Premios.Add(premio);
            }
            await _context.SaveChangesAsync();


            return classificados;
        }
        [HttpGet]
        [Route("Premio/Ganhador")]
        public async Task<ActionResult<Premio>> Ganhador()
        {
            var getPremiados = _context.Premios.OrderByDescending(i => i.ProvaPremio).ToListAsync();
            var aluno = await getPremiados;
            if (aluno == null)
            {
                return NotFound();
            }
            var ganhador = aluno.First();
            ganhador.Aluno = _context.Alunos.Find(ganhador.IdAluno);
            ganhador.Aluno.StatusAluno = "Ganhador do Prêmio";
            _context.Entry(ganhador.Aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ganhador;
        }


    }
}