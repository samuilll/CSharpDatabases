namespace TeamBuilder.Services
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Data;

    public class DatabaseInitializeService : IDatabaseInitializeService
    {
        public void InitializeDatabase()
        {
            using (var db = new TeamBuilderContext())
            {
               // db.Database.EnsureDeleted();

                db.Database.Migrate();
            }
        }
    }
}
