namespace alexn.Domain
{
    public class EntityBase<TIdentity>
    {
        public virtual TIdentity Id { get; set; }
        
        protected EntityBase(TIdentity id)
        {
            Id = id;    
        }

        protected EntityBase()
        {
            
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TIdentity>))  
                return false;  

            return (this == (EntityBase<TIdentity>)obj);  
        }  

        // Compare entities by Id
        public static bool operator ==(EntityBase<TIdentity> entity1, EntityBase<TIdentity> entity2)  
        {  
            object obj1 = entity1;  
            object obj2 = entity2;  

            if ((obj1 == null) && (obj2 == null))  
                return true;  

            if ((obj1 == null) || (obj2 == null))  
                return false;  

            if (entity1.GetType() != entity2.GetType())  
                return false;  

            return entity1.Id.Equals(entity2.Id);
        }  

        public static bool operator !=(EntityBase<TIdentity> entity1, EntityBase<TIdentity> entity2)  
        {  
            return (!(entity1 == entity2));  
        }  
 
        public override int GetHashCode()
        {
            return Id.GetHashCode();  
        }
    }
}