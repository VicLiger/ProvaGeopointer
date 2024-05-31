using ApiEquipament.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ApiEquipament.Context
{
    public class EquipamentoContext : DbContext
    {
        public EquipamentoContext(DbContextOptions<EquipamentoContext> options) : base(options) { }

        public DbSet<Equimento> Equipamentos { get; set; }
    }
}
