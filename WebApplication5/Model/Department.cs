﻿namespace WebApplication5.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List <Employee> Employees { get; set; } 

       
    }
}
