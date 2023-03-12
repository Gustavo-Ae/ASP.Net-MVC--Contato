using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio :  IContatoRepositorio
    {
        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            _context= bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            //Gravar no banco de dados;
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null)
            {
                throw new Exception("Houve um erro no momento de deletar o contato!");
            }

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();
            return true;

        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);
            
            if (contatoDB == null)
            {
                throw new Exception("Houve um erro na atualização do contato!");
            }

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contatos.Update(contatoDB); 
            _context.SaveChanges();
            return contatoDB;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _context.Contatos.ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}
