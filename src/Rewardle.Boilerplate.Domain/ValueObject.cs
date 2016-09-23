using System.Collections.Generic;
using System.Linq;
using Rewardle.Boilerplate.Domain.Services;

namespace Rewardle.Boilerplate.Domain
{
    public abstract class ValueObject
    {

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var vo = obj as ValueObject;
            return GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetEqualityComponents());
        }
    }
}