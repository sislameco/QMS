
using Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }

        [Column(Order = 101), Required]
        public EnumRStatus RStatus { get; set; }

        [Column(Order = 102), Required]
        public int CreatedBy { get; set; }

        [Column(Order = 103), Required]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 104)]
        public int? UpdatedBy { get; set; }

        [Column(Order = 105)]
        public DateTime? UpdatedDate { get; set; }

        [Column(Order = 106)]
        public int? DeletedBy { get; set; }

        [Column(Order = 107)]
        public DateTime? DeletedDate { get; set; }



        public event EventHandler<EntityChangedEventArgs> EntityChanged;

        public virtual void OnEntityCreated()
        {
            EntityChanged?.Invoke(this, new EntityChangedEventArgs(EntityChangeType.Created));
        }

        public virtual void OnEntityUpdated()
        {
            EntityChanged?.Invoke(this, new EntityChangedEventArgs(EntityChangeType.Updated));
        }

        public virtual void OnEntityDeleted()
        {
            EntityChanged?.Invoke(this, new EntityChangedEventArgs(EntityChangeType.Deleted));
        }
    }

    public enum EntityChangeType
    {
        Created,
        Updated,
        Deleted
    }

    public class EntityChangedEventArgs : EventArgs
    {
        public EntityChangeType ChangeType { get; }

        public EntityChangedEventArgs(EntityChangeType changeType)
        {
            ChangeType = changeType;
        }
    }
}
