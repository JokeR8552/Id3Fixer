using Id3Fixer.Application;
using Id3Fixer.Application.Parameters;
using Id3Fixer.Application.SongInfoGetter;
using Moq;


namespace Id3Fixer.Test;

[TestFixture]
public class FixerApplicationTests
{
    private Mock<IArgumentsProvider> argumentsProviderMock;
    private Mock<ISongInfoGetter> songInfoGetterMock;
    [SetUp]
    public void SetUp()
    {
        argumentsProviderMock = new Mock<IArgumentsProvider>();
        songInfoGetterMock = new Mock<ISongInfoGetter>();
    }


    [Test]
    public void Run_positive()
    {
        argumentsProviderMock.Setup(p => p.Parameters).Returns(new Parameters("a", "b"));

        var app = new FixerApplication(argumentsProviderMock.Object, songInfoGetterMock.Object);

        app.Run();
    }

}
