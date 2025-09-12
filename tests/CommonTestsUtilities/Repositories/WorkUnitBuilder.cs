using CoBudget.Domain.Repositories;
using Moq;

namespace CommonTestsUtilities.Repositories;
public class WorkUnitBuilder
{
    public static IWorkUnit Build()
    {
        var mock = new Mock<IWorkUnit>();

        return mock.Object;
    }
}
