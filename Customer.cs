using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    [Table("customer")]
    public class Customer
    {
        [PrimaryKey]
        [AutoIncrement]
        
        [Column("id")]
        public int Id { get; set; }
        
        [Column("taskname")]
        public string TaskName { get; set; }
        
        [Column("categoryname")]
        public string CategoryName { get; set; }

    }
}
