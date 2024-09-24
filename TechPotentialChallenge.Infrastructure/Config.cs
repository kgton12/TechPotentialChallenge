namespace TechPotentialChallenge.Infrastructure
{
    public class Config
    {
        public static readonly string ConnectionString = "Data Source=E:\\PROJETOS\\Estudo C#\\Projetos-DB\\TechPotentialChallengeDbContext.db";
        public static readonly Environment Environment = Environment.Development;
        public static readonly string HttpPath = "http://localhost:5281/Sale";
    }
    public enum Environment
    {
        Development,
        Production
    }
}
