using System.Text;

namespace Zyarat.Helpers
{
    public static class StringHelper
    {
        public static string ReplaceHolders(this string syntax,string replaced,string [] newValues)
        {
            var newString = new StringBuilder();
            var index = 0;
            for (var i = 0; i < syntax.Length; i++)
            {
                if (!syntax[i].Equals(replaced[0]))
                {
                    newString.Append(syntax[i]);
                }
                else
                {
                    newString.Append(newValues[index++]);
                    while (!syntax[i].Equals(replaced[1]))
                    {
                        i++;
                    }
                }
            }

            return newString.ToString();
        }
    }
}