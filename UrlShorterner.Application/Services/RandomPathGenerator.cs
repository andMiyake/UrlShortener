namespace UrlShorterner.Application.Services
{
    public class RandomPathGenerator
    {
        public RandomPathGenerator()
        {
        }

        public string GeneratePath()
        {
            Random res = new Random();


            string str = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 5;

            // Initializing the empty string
            string path = "";

            for (int i = 0; i < size; i++)
            {

                // Selecting a index randomly
                int x = res.Next(str.Length);

                // Appending the character at the 
                // index to the random alphanumeric string.
                path = path + str[x];
            }

            return path;
        }
    }
}