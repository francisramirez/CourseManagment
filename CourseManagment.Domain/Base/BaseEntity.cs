

namespace CourseManagment.Domain.Base
{
    public abstract class BaseEntity : Auditable
    {

        public virtual int Id { get; set; }
    }
}
