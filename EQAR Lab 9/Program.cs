using System;
using System.Collections.Generic;


interface IVisitor
{
    void Visit(Company company);
    void Visit(Department department);
    void Visit(Employee employee);
}


interface IVisitPlace
{
    void Accept(IVisitor visitor);
}

class Company : IVisitPlace
{
    public string Name { get; }
    public List<Department> Departments { get; }

    public Company(string name, List<Department> departments)
    {
        Name = name;
        Departments = departments;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        foreach (var department in Departments)
        {
            department.Accept(visitor);
        }
    }
}

class Department : IVisitPlace
{
    public string Name { get; }
    public List<Employee> Employees { get; }

    public Department(string name, List<Employee> employees)
    {
        Name = name;
        Employees = employees;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        foreach (var employee in Employees)
        {
            employee.Accept(visitor);
        }
    }
}

class Employee : IVisitPlace
{
    public string Position { get; }
    public double Salary { get; }

    public Employee(string position, double salary)
    {
        Position = position;
        Salary = salary;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}


class SalaryReportVisitor : IVisitor
{
    public void Visit(Company company)
    {
        Console.WriteLine($"Salary report of {company.Name} company for <29.12.2024>  -->");
    }

    public void Visit(Department department)
    {
        Console.WriteLine($"\nSalary report of {department.Name} department for <11.11.2024>  -->");
    }

    public void Visit(Employee employee)
    {
        Console.WriteLine($"Employee <#1>  Position -> {employee.Position} and salary -> {employee.Salary}$ ");
    }
}

class Program
{
    static void Main()
    {
    var animators = new List<Employee>
        {
         new Employee("Animator", 1500),
         new Employee("3D generalist", 2000),
         new Employee("3D rigger", 1700)
    };

    var coders = new List<Employee>
    {
         new Employee("C#", 1600),
         new Employee("QA", 1000),
         new Employee("Data", 1900)
    };


    var Animation_department = new Department("Motion and Design", animators);
    var IT_department = new Department("Code and  QA", coders);

    var company = new Company("Subutal", new List<Department> { Animation_department, IT_department });

    var salaryReportVisitor = new SalaryReportVisitor();
  
    company.Accept(salaryReportVisitor);

    Animation_department.Accept(salaryReportVisitor);
    }
}
