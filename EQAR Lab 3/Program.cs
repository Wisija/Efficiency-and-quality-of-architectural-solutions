using System;
using System.Text;

public class Director
{
    private IQueryConstructor constructor;

    public Director(IQueryConstructor constructor)
    {
        this.constructor = constructor;
    }

    public string BuildQuery(string columns, string table, string conditions, int limit)
    {
        return constructor.Select(columns).Table(table).Where(conditions).Limit(limit).GetQuery();
    }
}


public interface IQueryConstructor
{
    IQueryConstructor Select(string columns);
    IQueryConstructor Table(string table);
    IQueryConstructor Where(string conditions);
    IQueryConstructor Limit(int limit);
    string GetQuery();
}





public class MySQLConstructor : IQueryConstructor
{
    private StringBuilder query;

    public MySQLConstructor()
    {
        query = new StringBuilder();
    }

    public IQueryConstructor Select(string columns)
    {
        query.Append("SELECT ")
             .Append(columns);
        return this;
    }
    public IQueryConstructor Table(string conditions)
    {
        query.Append(" FROM  ")
             .Append(conditions);
        return this;
    }

    public IQueryConstructor Where(string conditions)
    {
        query.Append(" WHERE ").Append(conditions);
        return this;
    }

    public IQueryConstructor Limit(int limit)
    {
        query.Append(" LIMIT ").Append(limit);
        return this;
    }

    public string GetQuery()
    {
        return query.Append(";").ToString();
    }
}

public class PostgreSQLConstructor : IQueryConstructor
{
    private StringBuilder query;

    public PostgreSQLConstructor()
    {
        query = new StringBuilder();
    }

    public IQueryConstructor Select(string columns)
    {
        query.Append("SELECT ")
             .Append(columns);
        return this;
    }

    public IQueryConstructor Table(string conditions)
    {
        query.Append(" FROM  ")
             .Append(conditions);
        return this;
    }

    public IQueryConstructor Where(string conditions)
    {
        query.Append(" WHERE ").Append(conditions);
        return this;
    }

    public IQueryConstructor Limit(int limit)
    {
        query.Append(" LIMIT ").Append(limit);
        return this;
    }

    public string GetQuery()
    {
        return query.Append(";").ToString();
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        IQueryConstructor postgresBuilder = new PostgreSQLConstructor();
        Director postgresDirector = new Director(postgresBuilder);
        string postgresQuery = postgresDirector.BuildQuery("name, age","Customers", "Bonus amount > 50", 40);
        Console.WriteLine("PostgreSQL ->     " + postgresQuery);

        IQueryConstructor mysqlBuilder = new MySQLConstructor();
        Director mysqlDirector = new Director(mysqlBuilder);
        string mysqlQuery = mysqlDirector.BuildQuery("name, age", "Customers", "Bonus amount > 50", 40);
        Console.WriteLine("MySQL ->     " + mysqlQuery);
    }
}