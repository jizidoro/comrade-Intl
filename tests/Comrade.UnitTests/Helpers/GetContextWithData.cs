#region

using Comrade.Infrastructure.DataAccess;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public class GetContextWithData
    {
        public ComradeContext Excute(ComradeContext context)
        {
            context.SaveChanges();

            return context;
        }
    }
}