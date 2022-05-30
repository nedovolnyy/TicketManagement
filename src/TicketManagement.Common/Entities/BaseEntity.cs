using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public abstract class BaseEntity : IEqualityComparer<BaseEntity>
    {
        public int? Id { get; protected set; }
        protected abstract bool IsNull(BaseEntity entity);
        protected abstract string ForEquals(BaseEntity entity);

        public bool IsEmpty(BaseEntity entity) =>
            IsNull(entity);

        public string StringForEquals(BaseEntity entity) =>
            ForEquals(entity);

        public bool Equals(BaseEntity entity, BaseEntity tmpEntity)
        {
            if (entity.IsEmpty() && tmpEntity.IsEmpty())
            {
                return true;
            }

            if (entity.IsEmpty() || tmpEntity.IsEmpty())
            {
                return false;
            }

            if (entity.StringForEquals() == tmpEntity.StringForEquals())
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BaseEntity entity)
        {
            if (entity.IsEmpty())
            {
                return 0;
            }

            return entity.StringForEquals().GetHashCode();
        }
    }
}
