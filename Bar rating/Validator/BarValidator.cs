using Bar_rating.Models;
namespace Bar_rating.Validator
{
    public static class BarValidator
    {
        public static string ReturnErrorsCreate(List<Bar> bars,Bar bar)
        {
            string msg = "";
            if (String.IsNullOrWhiteSpace(bar.Name) || String.IsNullOrWhiteSpace(bar.Description))
            {
                msg += "Попълнете всички полета!\n";
            }
   
            if (bars.FirstOrDefault(p => p.Name == bar.Name) != null)
            {
                msg += "Това име вече присъства в базата с барове!";
            }
            return msg;
        }
        public static string ReturnErrorsEdit(List<Bar> bars, Bar bar, int id)
        {
            string msg = "";
            if (String.IsNullOrWhiteSpace(bar.Name) || String.IsNullOrWhiteSpace(bar.Description))
            {
                msg += "Попълнете всички полета!\n";
            }
            if (bars.FirstOrDefault(p => p.Name == bar.Name && p.Id != id) != null)
            {
                msg += "Това име вече присъства в базата с барове!\n";
            }
            return msg;
        }
    }
}
