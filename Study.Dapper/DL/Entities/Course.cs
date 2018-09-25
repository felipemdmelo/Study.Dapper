using Dapper.Contrib.Extensions;

namespace Study.Dapper.DL.Entities
{
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long DepartmentId { get; set; }
        [Write(false)]
        public virtual Department Department { get; set; }
    }
}
