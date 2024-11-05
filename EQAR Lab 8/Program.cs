using System;

namespace TemplateMethodPattern
{
    public abstract class EntityUpdate
    {
        public void UpdateEntity()
        {
            var entity = GetEntity();
            if (ValidateData(entity))
            {
                SaveData(entity);
                Console.Write("Status ->");
                SendResponse(200, "Success", SuccsessMessage());
                Console.Write("\n");
            }
            else
            {
                ValidationFailure(entity);
                Console.Write("Status ->");
                SendResponse(400, "Failure", FailureMessage());
                Console.Write("\n");
            }
        }

        public abstract object GetEntity();
        protected abstract bool ValidateData(object entity);
        protected abstract void SaveData(object entity);
        protected abstract void ValidationFailure(object entity);

        protected virtual object SuccsessMessage()
        { 
            return "OK";
        }

        protected virtual object FailureMessage()
        {
            return "FAIL";
        }

        private void SendResponse(int statusCode, string status, object additionalData)
        {
            Console.WriteLine($"{statusCode}, {status}, {additionalData}");
        }
    }

    public class ProductUpdate : EntityUpdate
    {
        public override object GetEntity()
        {
            return new Product();
        }

        protected override bool ValidateData(object entity)
        {
            return true; 
        }

        protected override void SaveData(object entity)
        {
            Console.WriteLine("Product saving -> ");
        }

        protected override void ValidationFailure(object entity)
        {
            Console.WriteLine("Sended to messenger ");
        }

    }

    public class UserUpdateProcessor : EntityUpdate
    {
        public override object GetEntity()
        {
            return new User();
        }

        protected override bool ValidateData(object entity)
        {
            return true;
        }

        protected override void SaveData(object entity)
        {
            Console.WriteLine("User data saving (without email update)");
        }

        protected override void ValidationFailure(object entity)
        {
            Console.WriteLine("User validation failure");
        }
        protected override object FailureMessage()
        {
            return "Validation Failed";
        }
    }

 
    public class OrderUpdateProcessor : EntityUpdate
    {
        public override object GetEntity()
        {
            return new Order();
        }

        protected override bool ValidateData(object entity)
        {
            return false;
        }

        protected override void SaveData(object entity)
        {
            Console.WriteLine("Saving order data");
        }

        protected override void ValidationFailure(object entity)
        {
            Console.WriteLine("Handling order validation failure");
        }

        protected override object SuccsessMessage()
        {
            return "Order.JSON";
        }
        protected override object FailureMessage()
        {
            return "Validation Failed";
        }

    }



    public class Product { }
    public class User { }
    public class Order { }


    public class Program
    {
        public static void Main()
        {
            EntityUpdate productProcessor = new ProductUpdate();
            productProcessor.UpdateEntity();

            EntityUpdate userProcessor = new UserUpdateProcessor();
            userProcessor.UpdateEntity();

            EntityUpdate orderProcessor = new OrderUpdateProcessor();
            orderProcessor.UpdateEntity();
        }
    }
}
