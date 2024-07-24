
using Bar_rating.Models;

namespace Bar_rating.Suppliers
{
    public static class RoleSupplier
    {
        public static string role = "Empty";


        public  static void GetRole(User user)
        {
            if (user.IsAdmin == true)
            {
                RoleSupplier.role = "Admin";
            }
            else
            {
                RoleSupplier.role = "User";
            }
        }
    }
}
