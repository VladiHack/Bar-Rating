using Bar_rating.Models;

namespace Bar_rating.Validator
{
    public static class BarValidator
    {

        public static async Task<string> ReturnErrorsCreateAsync(List<Bar> bars,Bar bar)
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
            if (bar.BarImageFile != null && bar.BarImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await bar.BarImageFile.CopyToAsync(memoryStream);
                    bar.BarImage = memoryStream.ToArray();
                }
            }
            return msg;
        }
        public static async Task<string> ReturnErrorsEditAsync(List<Bar> bars, Bar bar, int id)
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
        public static async Task UpdateImage(Bar oldBar, Bar bar)
        {

            if (bar.BarImageFile != null && bar.BarImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await bar.BarImageFile.CopyToAsync(memoryStream);
                    bar.BarImage = memoryStream.ToArray();
                }
            }
            else
            {
                bar.BarImage = oldBar.BarImage;
            }

        }
    }
}
