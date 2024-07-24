using Bar_rating.Models;
namespace Bar_rating.Validator
{
    public static class UserValidator
    {
        public static string ReturnErrorsCreate(List<User> users,User user)
        {
            string msg = "";
            if(String.IsNullOrWhiteSpace(user.Username)||String.IsNullOrWhiteSpace(user.FirstName)||String.IsNullOrWhiteSpace(user.LastName)||String.IsNullOrWhiteSpace(user.Password))
            {
                msg += "Попълнете всички полета!\n";
            }
            if(user.Password.Length<6)
            {
                msg += "Паролата трябва да е поне 6 знака";
            }
            if (users.FirstOrDefault(p => p.Username == user.Username) != null)
            {
                msg += "Това потребителско име вече присъства в базата със служители!";
            }
            if (users.FirstOrDefault(p => p.FirstName == user.FirstName && p.LastName == user.LastName) != null)
            {
                msg += "Това име вече присъства в базата със служители!\n";
            }
            return msg;
        }
        public static string ReturnErrorsEdit(List<User> users, User user, int id)
        {
            //Changed user.Id 
            string msg = "";
            if (String.IsNullOrWhiteSpace(user.Username) || String.IsNullOrWhiteSpace(user.FirstName) || String.IsNullOrWhiteSpace(user.LastName) || String.IsNullOrWhiteSpace(user.Password))
            {
                msg += "Попълнете всички полета!\n";
            }
            if (user.Password.Length < 6)
            {
                msg += "Паролата трябва да е поне 6 знака";
            }
            if (users.FirstOrDefault(p => p.FirstName == user.FirstName  && p.LastName == user.LastName && p.Id != id) != null)
            {
                msg += "Това име вече присъства в базата със служители!\n";
            }
            if (users.FirstOrDefault(p => p.Username == user.Username && p.Id != user.Id) != null)
            {
                msg += "Това потребителско име вече присъства в базата със служители!";
            }
            return msg;
        }
        public static void AddIdToUser(List<User> users,User user)
        {
            if (users.Count == 0)
            {
                user.Id = 1;
            }
            else
            {
                user.Id = users[users.Count() - 1].Id + 1;
            }
        }

        public static void AssignAdminRoleIfUserListIsEmpty(List<User> users,User user)
        {

            if (users.Count == 0)
            {
                user.IsAdmin = true;
            }
            else
            {
                user.IsAdmin = false;
            }
        }
    }
}
