using Zyarat.Data;

namespace Zyarat.Models.Repositories
{
    public abstract class ContextRepo
    {
        protected  readonly ApplicationContext Context;

        protected ContextRepo(ApplicationContext context)
        {
            Context = context;
        }
    }
}